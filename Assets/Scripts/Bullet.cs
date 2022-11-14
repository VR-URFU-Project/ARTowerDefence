using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Arrow,
        Bullet
    }

    public BulletType type;

    private Transform target;

    public double speed = 5d;

    private int _damage;

    private void Start()
    {
        switch (type)
        {
            case BulletType.Arrow:
                _damage= TowerManager.GetTreeHouse().Damage;
                break;

            case BulletType.Bullet:
                _damage = TowerManager.GetBallista().Damage;
                break;
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = (float)speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Debug.Log("Hitted the target");
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform targetEnemy)
    {
        EnemyScript e = targetEnemy.transform.parent.GetComponent<EnemyScript>();

        if (e != null)
            e.TakeDamage(_damage);
    }
}
