using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet> bulletPool;

    private Canon canonScript;

    private void Start()
    {
        canonScript = GetComponent<Canon>();
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet,
                                            true, defaultCapacity:1000, maxSize:10000);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(canonScript.bullet, canonScript.firePoint.position, canonScript.firePoint.rotation, canonScript.parent.transform);

        bullet.SetPool(bulletPool);

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        // set the transform
        bullet.gameObject.transform.parent = canonScript.parent.transform;
        bullet.gameObject.transform.position = canonScript.firePoint.position;
        bullet.gameObject.transform.rotation = canonScript.firePoint.rotation;

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
