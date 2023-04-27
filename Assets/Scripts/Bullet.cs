using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Arrow,
        Bullet
    }

    public BulletType type;

    private Transform target;

    public double speed = 0.4d;

    private int _damage;

    private ObjectPool<Bullet> bulletPool;

    private void OnEnable()
    {
        switch (type)
        {
            case BulletType.Arrow:
                _damage = TowerManager.GetTreeHouse().Damage;
                speed = TowerManager.GetTreeHouse().ProjectileSpeed;
                break;

            case BulletType.Bullet:
                _damage = TowerManager.GetBallista().Damage;
                speed = TowerManager.GetBallista().ProjectileSpeed;
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
            bulletPool.Release(this);
            //Destroy(gameObject);
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
        transform.Rotate(180f, 0, 0);
    }

    void HitTarget()
    {
       // Debug.Log("Hitted the target");
        Damage(target);
        //Destroy(gameObject);
        bulletPool.Release(this);
    }

    void Damage(Transform targetEnemy)
    {
        //Debug.Log("Damage from Bullet");

        EnemyScript e = targetEnemy.transform.parent.GetComponent<EnemyScript>();
        if (e != null)
            e.TakeDamage(_damage);
    }

    public void SetPool(ObjectPool<Bullet> bulletPool)
    {
        this.bulletPool = bulletPool;
    }
}
