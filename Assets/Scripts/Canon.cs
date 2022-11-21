using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Tower Info")]
    public TowerType towerType = TowerType.Ballista;
    public TowerData Tdata;

    private Transform target = null;
    private GameObject parent;

    [Header("Attributes")]
    [SerializeField]
    private double fireCountdown = 0f;

    private bool partSys_isON = false;

    [Header("Unity Setup Fields")]
    public string TargetTag = "Target";
    public string EnemyTag = "Enemy";
    public string FlyEnemyTag = "Fly";

    public Transform partToRotate;
    public float turnSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private GameObject[] particleSystems;

    private bool ifEnemiesNearBy = false;

    [Header("Special Settings")]
    private const double _scale = 0.1;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        parent = GameObject.FindGameObjectWithTag("GamingPlace");
        particleSystems = GameObject.FindGameObjectsWithTag("Particle_System");
        foreach (var go in particleSystems) go.GetComponent<ParticleSystem>().Stop();
    }

    void UpdateTarget()
    {
        switch (Tdata.Type)
        {
            case TowerType.Ballista:
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTag);
                    float shortestDistance = Mathf.Infinity;
                    GameObject nearestEnemy = null;

                    foreach (GameObject enemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy < shortestDistance)
                        {
                            shortestDistance = distanceToEnemy;
                            nearestEnemy = enemy;
                        }

                    }

                    if (nearestEnemy != null && shortestDistance <= Tdata.Range * _scale)
                    {
                        target = nearestEnemy.transform;
                    }
                    else
                    {
                        target = null;
                    }
                    return;
                }
            case TowerType.Mushroom:
                {
                    //particleSystems = GameObject.FindGameObjectsWithTag("Particle_System");
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
                    GameObject[] fly_enemies = GameObject.FindGameObjectsWithTag(FlyEnemyTag);

                    if (fly_enemies.Length > 0)
                    {
                        //if (!partSys_isON) partSys_isON = true;
                        //else partSys_isON = false;
                        if (!partSys_isON)
                        {
                            partSys_isON = true;
                            //foreach (var go in particleSystems) go.SetActive(true);
                        }
                        //else
                        //{
                        //    partSys_isON = false;
                        //    foreach (var go in particleSystems) go.SetActive(false);
                        //}

                        foreach (GameObject fly in fly_enemies)
                        {
                            var radius = Tdata.Range * _scale;
                            if (Vector3.Distance(transform.position, fly.transform.position) <= radius)
                            {
                                ifEnemiesNearBy = true;
                                //Explode();
                            }
                        }
                    }
                    else
                    {
                        ifEnemiesNearBy = false;
                        partSys_isON = false;
                    }

                    if (enemies.Length > 0)
                    {
                        //if (!partSys_isON) partSys_isON = true;
                        //else partSys_isON = false;
                        if (!partSys_isON)
                        {
                            partSys_isON = true;
                            //foreach (var go in particleSystems) go.SetActive(true);
                        }
                        //else
                        //{

                        //    //foreach (var go in particleSystems) go.SetActive(false);
                        //}

                        foreach (GameObject enemy in enemies)
                        {
                            var radius = Tdata.Range * _scale;
                            if (Vector3.Distance(transform.position, enemy.transform.position) <= radius)
                            {
                                ifEnemiesNearBy = true;
                                //Explode();
                            }
                        }
                    }
                    else
                    {
                        ifEnemiesNearBy = false;
                        partSys_isON = false;
                    }
                    return;
                }
        }
    }

    void Update()
    {
        if (partSys_isON) foreach (var go in particleSystems) go.GetComponent<ParticleSystem>().Play();
        else foreach (var go in particleSystems) go.GetComponent<ParticleSystem>().Stop();

        if (target == null && !ifEnemiesNearBy) return;
        else
        {
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotationVector = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f, rotationVector.y, 0f);

                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1d / Tdata.AtackSpeed;
                }

                fireCountdown -= Time.deltaTime;
            }

            if (ifEnemiesNearBy)
            {
                if (fireCountdown <= 0f)
                {
                    Explode();
                    fireCountdown = 1d / Tdata.AtackSpeed;
                }


               fireCountdown -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        //Debug.Log("SHHHHOOOOOOOOOOTTTTTTT!!!");
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, parent.transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Explode()
    {
        Debug.Log("EXPLOOOOODEEE!!!");
        Collider[] colliders = Physics.OverlapSphere(transform.position, (float)(Tdata.Range * _scale));
        foreach(var collider in colliders)
        {
            if (collider.tag == EnemyTag || collider.tag == FlyEnemyTag)
            {
                Damage(collider.transform);
            }
        }
        //partSys_isON = false;
    }

    void Damage(Transform enemy)
    {
        Debug.Log("DAMAGEEEE!!");
        enemy.GetComponent<EnemyScript>().TakeDamage(Tdata.Damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, (float)(Tdata.Range * _scale));
    }
}
