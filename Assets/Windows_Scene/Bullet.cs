using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public int _damage = 20;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

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
