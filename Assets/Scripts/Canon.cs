using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public enum TowerType
    {
        Ballista,
        Mushroom,
        AirCanon,
        TreeHouse,
        LazerTower
    }

    [Header("Tower Type")]
    public TowerType towerType = TowerType.Ballista;

    private Transform target;
    // вторая цель для дерева с лучниками
    //private Transform target_2;
    private GameObject parent;

    [Header("Attributes")]
    [SerializeField]
    private float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string EnemyTag = "Enemy";
    public string FlyEnemyTag = "Fly";

    public Transform partToRotate;
    public float turnSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        parent = GameObject.FindGameObjectWithTag("GamingPlace");
    }

    void UpdateTarget()
    {
        switch (towerType)
        {
            case TowerType.Ballista:
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
                    float shortestDistance = Mathf.Infinity;
                    GameObject nearestEnemy = null;

                    foreach (GameObject enemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        //new Vector3(enemy.transform.position.x,
                        //             enemy.transform.position.y + 2.96f,
                        //             enemy.transform.position.z));
                        if (distanceToEnemy < shortestDistance)
                        {
                            shortestDistance = distanceToEnemy;
                            nearestEnemy = enemy;
                        }

                    }

                    if (nearestEnemy != null && shortestDistance <= range)
                    {
                        target = nearestEnemy.transform;
                    }
                    else
                    {
                        target = null;
                    }
                    return;
                }
            case TowerType.AirCanon:
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
                    GameObject[] fly_enemies = GameObject.FindGameObjectsWithTag(FlyEnemyTag);
                    float shortestDistance = Mathf.Infinity;
                    float fly_shortestDistance = Mathf.Infinity;
                    GameObject fly_nearestEnemy = null;
                    GameObject nearestEnemy = null;

                    foreach (GameObject fly in fly_enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, fly.transform.position);
                        if (distanceToEnemy < fly_shortestDistance)
                        {
                            fly_shortestDistance = distanceToEnemy;
                            fly_nearestEnemy = fly;
                        }
                    }

                    foreach (GameObject enemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy < shortestDistance)
                        {
                            shortestDistance = distanceToEnemy;
                            nearestEnemy = enemy;
                        }
                    }

                    if (nearestEnemy != null || fly_nearestEnemy != null)
                        if (shortestDistance <= range || fly_shortestDistance <= range) {
                            if (shortestDistance < fly_shortestDistance)
                                target = nearestEnemy.transform;
                            else
                                target = fly_nearestEnemy.transform;
                            }
                    //if (nearestEnemy != null && (shortestDistance <= range || fly_shortestDistance <= range))
                    //{
                    //    target = nearestEnemy.transform;
                    //}
                    else
                    {
                        target = null;
                    }
                    return;
                }
            //case TowerType.TreeHouse:
            //    {
            //        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
            //        GameObject[] fly_enemies = GameObject.FindGameObjectsWithTag(FlyEnemyTag);
            //        float shortestDistance = Mathf.Infinity;
            //        float fly_shortestDistance = Mathf.Infinity;
            //        GameObject fly_nearestEnemy_1 = null;
            //        GameObject fly_nearestEnemy_2 = null;
            //        GameObject nearestEnemy_1 = null;
            //        GameObject nearestEnemy_2 = null;

            //        foreach (GameObject fly in fly_enemies)
            //        {
            //            float distanceToEnemy = Vector3.Distance(transform.position, fly.transform.position);
            //            if (distanceToEnemy < fly_shortestDistance)
            //            {
            //                fly_shortestDistance = distanceToEnemy;
            //                fly_nearestEnemy_1 = fly;
            //            }
            //        }

            //        foreach (GameObject enemy in enemies)
            //        {
            //            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //            if (distanceToEnemy < shortestDistance)
            //            {
            //                shortestDistance = distanceToEnemy;
            //                nearestEnemy = enemy;
            //            }
            //        }

            //        if (nearestEnemy != null || fly_nearestEnemy != null)
            //            if (shortestDistance <= range || fly_shortestDistance <= range)
            //            {
            //                if (shortestDistance < fly_shortestDistance)
            //                    target = nearestEnemy.transform;
            //                else
            //                    target = fly_nearestEnemy.transform;
            //            }
            //            //if (nearestEnemy != null && (shortestDistance <= range || fly_shortestDistance <= range))
            //            //{
            //            //    target = nearestEnemy.transform;
            //            //}
            //            else
            //            {
            //                target = null;
            //            }
            //        return;
            //  }
            //default: Debug.LogError(@$"Tower {gameObject.name} need to be connected with the appropriate type!");
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotationVector = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotationVector.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
