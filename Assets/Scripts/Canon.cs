using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Tower Info")]
    public TowerType towerType = TowerType.Ballista;
    public TowerData Tdata;

    private Transform target;
    private GameObject parent;

    [Header("Attributes")]
    [SerializeField]
    private double fireCountdown = 0f;

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
        switch (Tdata.Type)
        {
            case TowerType.Ballista:
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
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

                    if (nearestEnemy != null && shortestDistance <= Tdata.Range)
                    {
                        target = nearestEnemy.transform;
                    }
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
            //        GameObject fly_nearestEnemy = null;
            //        GameObject nearestEnemy = null;

            //        foreach (GameObject fly in fly_enemies)
            //        {
            //            float distanceToEnemy = Vector3.Distance(transform.position, fly.transform.position);
            //            if (distanceToEnemy < fly_shortestDistance)
            //            {
            //                fly_shortestDistance = distanceToEnemy;
            //                fly_nearestEnemy = fly;
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
            //            if (shortestDistance <= Tdata.Range || fly_shortestDistance <= Tdata.Range)
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
            //    }
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
            fireCountdown = 1d / Tdata.AtackSpeed;
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
        Gizmos.DrawWireSphere(transform.position, (float)Tdata.Range);
    }
}
