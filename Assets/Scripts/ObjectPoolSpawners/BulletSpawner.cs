using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet> bulletPool;

    [SerializeField]
    public List<Transform> firePoints;

    private Canon canonScript;

    private int previous = -1;

    private void Start()
    {
        canonScript = GetComponent<Canon>();
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet,
                                            true, defaultCapacity:1000, maxSize:10000);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(canonScript.bullet, firePoints[0].position, firePoints[0].rotation, canonScript.parent.transform);

        bullet.SetPool(bulletPool);

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        // set the transform
        bullet.gameObject.transform.parent = canonScript.parent.transform;

        int a;

        if (firePoints.Count == 1)
            a = 0;
        else
        {
            do
            {
                a = Random.Range(0, firePoints.Count);
            } while (a == previous);
        }

        bullet.gameObject.transform.position = firePoints[a].position;
        bullet.gameObject.transform.rotation = firePoints[a].rotation;

        // activate GO
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
