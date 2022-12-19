using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Tower Info")]
    public TowerData Tdata;

    private Transform target = null;
    private GameObject parent;

    [Header("Attributes")]
    [SerializeField]
    private double fireCountdown = 0d;

    private bool partSys_isON = false;

    [Header("Audio")]
    [SerializeField] AudioClip ShootSound;
    AudioSource audio;

    [Header("Unity Setup Fields")]
    public string TargetTag = "Target";
    public string EnemyTag = "Enemy";
    public string FlyEnemyTag = "Fly";

    public Transform partToRotate;
    public float turnSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("TreeHouse Setup Fileds")]
    //public GameObject archer_1;
    //public GameObject archer_2;
    [SerializeField] public TowerType towerType;

    [Header("Lazer Settings")]
    private LineRenderer lineRenderer;
    private bool useLazer = false;
    private int secCounter = 1;

    // флаг для дерева с лучниками
    //private bool treeIsAtacking = false;

    private ParticleSystem[] particleSystems;

    private bool ifEnemiesNearBy = false;

    [Header("Special Settings")]
    private const double _scale = 0.1;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        parent = GameObject.FindGameObjectWithTag("GamingPlace");
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        foreach (var go in particleSystems) go.Stop();

        if (towerType == TowerType.TreeHouse && Tdata == null)
        {
            Tdata = TowerManager.GetTreeHouse();
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        //GameObject[] fly_enemies = GameObject.FindGameObjectsWithTag(FlyEnemyTag);
        GameObject nearestEnemy;

        switch (Tdata.Type)
        {
            case TowerType.Ballista:
                nearestEnemy = GetNearestAvailableEnemy(enemies);

                // проверка на наземного врага
                    if (nearestEnemy != null)
                    {
                        if (nearestEnemy.transform.parent.tag == EnemyTag)
                        {
                            target = nearestEnemy.transform;
                        }
                    }
                    else
                        target = null;
                
                break;

            case TowerType.TreeHouse:
                nearestEnemy = GetNearestAvailableEnemy(enemies/*.Concat(fly_enemies).ToArray()*/);

                if (nearestEnemy != null)
                    target = nearestEnemy.transform;
                else
                    target = null;
                break;

            case TowerType.Mushroom:
                if (/*fly_enemies.Length > 0 ||*/ enemies.Length > 0)
                {
                    ifEnemiesNearBy = false;
                    var radius = Tdata.Range * _scale;
                        //foreach (GameObject fly in fly_enemies)
                        //{
                        //    if (Vector3.Distance(transform.position, fly.transform.position) <= radius)
                        //    {
                        //        ifEnemiesNearBy = true;
                        //    }
                        //}

                        foreach (GameObject enemy in enemies)
                        {
                            if (Vector3.Distance(transform.position, enemy.transform.position) <= radius)
                            {
                                ifEnemiesNearBy = true;
                            }
                        }

                    partSys_isON = ifEnemiesNearBy;
                }
                else
                {
                    ifEnemiesNearBy = false;
                    partSys_isON = false;
                }
                break;
            case TowerType.LazerTower:
                nearestEnemy = GetNearestAvailableEnemy(enemies/*.Concat(fly_enemies).ToArray()*/);

                if (nearestEnemy != null)
                {
                    target = nearestEnemy.transform;
                    useLazer = true;
                }
                else
                {
                    target = null;
                }

                break;
        }
    }

    private GameObject GetNearestAvailableEnemy(GameObject[] enemies)
    {
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (shortestDistance <= Tdata.Range * _scale)
            return nearestEnemy;
        return null;
    }

    void Update()
    {
        if (partSys_isON)
        {
            //Debug.Log("partSys_isON");
            foreach (var go in particleSystems)
            {
                if (!go.isPlaying)
                    go.Play();
            }
        }
        else
        {
            foreach (var go in particleSystems) go.Stop();
        }

        if (target == null && !ifEnemiesNearBy)
        {
            if (useLazer)
            {
                //if (lineRenderer.enabled)
                //{
                    lineRenderer.enabled = false;
                //}
            }
            secCounter = 1;
            return;
        }
        else
        {
            if (ifEnemiesNearBy)
            {
                if (fireCountdown <= 0f)
                {
                    Explode();
                    fireCountdown = 1d / Tdata.AtackSpeed;
                }
            }

            if (target != null)
            {
                void LookOnTarget()
                {
                    Vector3 direction = target.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    Vector3 rotationVector = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                    partToRotate.rotation = Quaternion.Euler(0f, rotationVector.y, 0f);
                }

                if (useLazer)
                {
                    UseLazer();
                    if (fireCountdown <= 0f)
                    {
                        Damage(target.parent.transform);
                        fireCountdown = 1d / Tdata.AtackSpeed;

                        if (secCounter < 10) secCounter += 1;
                    }
                }
                else
                {
                    LookOnTarget();
                    if (fireCountdown <= 0f)
                    {
                        Shoot();
                        fireCountdown = 1d / Tdata.AtackSpeed;
                    }
                }

            }
            fireCountdown -= Time.deltaTime;

        }
    }

    void UseLazer()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        audio.PlayOneShot(ShootSound, 0.99f);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, parent.transform);
        audio.PlayOneShot(ShootSound);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Explode()
    {
        //Debug.Log("EXPLOOOOODEEE!!!");
        audio.PlayOneShot(ShootSound, 0.99f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, (float)(Tdata.Range * _scale));
        foreach (var collider in colliders)
        {
            if (collider.tag == EnemyTag || collider.tag == FlyEnemyTag)
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {

        if (useLazer)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(Tdata.Damage * secCounter);
            Debug.Log($"Lazer: {Tdata.Damage * secCounter}!!");
        }
        else
        {
            //Debug.Log("DAMAGEEEE!!");
            enemy.GetComponent<EnemyScript>().TakeDamage(Tdata.Damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, (float)(Tdata.Range * _scale));
    }
}
