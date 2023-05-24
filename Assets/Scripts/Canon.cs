using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Tower Info")]
    public TowerData Tdata;

    private List<Transform> targets = null;
    public GameObject parent;

    [Header("Audio")]
    [SerializeField] AudioClip ShootSound;
    new AudioSource audio;

    [Header("Unity Setup Fields")]
    public string TargetTag = "Target";
    public string EnemyTag = "Enemy";
    public string FlyEnemyTag = "Fly";

    public Bullet bullet;
    public Transform firePoint;

    private BulletSpawner bulletSpawner;

    [Header("Lazer Settings")]
    private LineRenderer lineRenderer;
    private int secCounter = 1;

    [Header("Shroom Settings")]
    private ParticleSystem[] particleSystems;
    private bool ifEnemiesNearBy = false;

    [Header("Special Settings")]
    private const double _scale = 0.05;
    private double fireCountdown = 0d;

    void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        parent = GameObject.FindGameObjectWithTag("GamingPlace");
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        foreach (var go in particleSystems) go.Stop();

        //if (towerType == TowerType.TreeHouse && Tdata == null)
        //{
        //    Tdata = TowerManager.GetTreeHouse();
        //}
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        //GameObject nearestEnemy = null;
        var nearestEnemys = new List<GameObject>();

        switch (Tdata.Type)
        {
            case TowerType.Ballista:
                nearestEnemys = GetNearestAvailableEnemys(
                    enemies
                    .Where(x => x.transform.parent.tag == EnemyTag)
                    .ToArray());
                break;

            case TowerType.TreeHouse:
                nearestEnemys = GetNearestAvailableEnemys(enemies);
                break;

            case TowerType.Mushroom:
                ifEnemiesNearBy = false;
                var radius = Tdata.Range * _scale;

                foreach (GameObject enemy in enemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= radius)
                        ifEnemiesNearBy = true;
                }
                break;

            case TowerType.LazerTower:
                nearestEnemys = GetNearestAvailableEnemys(enemies);
                break;
        }

        if (nearestEnemys != null)
            targets = nearestEnemys.Select(x => x.transform).ToList();
        else
            targets = null;
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

    private List<GameObject> GetNearestAvailableEnemys(GameObject[] enemies)
    {
        var nearestEnemys = new List<GameObject>();

        var a = enemies
            .Where(x => Vector3.Distance(transform.position, x.transform.position) <= Tdata.Range * _scale)
            .ToList();
        a.Sort((x, y) =>
        Vector3.Distance(transform.position, x.transform.position)
        .CompareTo(Vector3.Distance(transform.position, y.transform.position)));
        
        if(a.Count == 0) return null;

        for (int i=0; i< Tdata.TargetsAmount; ++i)
            nearestEnemys.Add(a[i % a.Count]);
        return nearestEnemys;
    }

    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (ifEnemiesNearBy)
        {
            foreach (var go in particleSystems)
                if (!go.isPlaying) go.Play();

            if (fireCountdown <= 0f)
            {
                Explode();
                fireCountdown = 1d / Tdata.AtackSpeed;
            }
        }
        else
        {
            foreach (var go in particleSystems) go.Stop();
        }

        if (targets == null)
        {
            if (Tdata.Type == TowerType.LazerTower)
            {
                lineRenderer.enabled = false;
                secCounter = 1;
            }
            return;
        }

        if (Tdata.Type == TowerType.LazerTower)
        {
            UseLazer();
            if (fireCountdown <= 0f)
            {
                audio.PlayOneShot(ShootSound, 0.99f);
                foreach(var target in targets)
                    Damage(target.parent.transform);
                fireCountdown = 1d / Tdata.AtackSpeed;

                if (secCounter < 10) secCounter += 1;
            }
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1d / Tdata.AtackSpeed;
            }
        }
    }

    void UseLazer()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.positionCount = targets.Count * 2;
        for(int i=0; i< targets.Count; ++i)
        {
            lineRenderer.SetPosition(i*2, firePoint.position);
            lineRenderer.SetPosition(i*2+1, targets[i].position);
        }
    }

    void Shoot()
    {
        // TODO заменить Instantiate на Pulling.get();
        //GameObject bulletGO = Instantiate(bullet, firePoint.position, firePoint.rotation, parent.transform);
        audio.PlayOneShot(ShootSound);
        //Bullet bullet = bulletGO.GetComponent<Bullet>();

        foreach (var target in targets)
        {
            Bullet bullet = bulletSpawner.bulletPool.Get();

            if (bullet != null)
            {
                //var speed = Tdata.ProjectileSpeed;
                //if (speed > 0)
                //    bullet.speed = Tdata.ProjectileSpeed;
                bullet.Seek(target);
            }
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
                Damage(collider.transform);
        }
    }

    void Damage(Transform enemy)
    {

        if (Tdata.Type == TowerType.LazerTower)
            enemy.GetComponent<EnemyScript>().TakeDamage(Tdata.Damage * secCounter);
        else
            enemy.GetComponent<EnemyScript>().TakeDamage(Tdata.Damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, (float)(Tdata.Range * _scale));
    }

    public Transform GetTarget()
    {
        if (targets == null)
            return null;
        else
            return targets[0];
    }
}
