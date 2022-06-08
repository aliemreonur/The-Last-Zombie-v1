using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    [SerializeField] private Bullet _bulletPrefab;

    public List<Bullet> bulletPool = new List<Bullet>();
    private int numberOfBullets = 20;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBullets(numberOfBullets);
    }

    List<Bullet> GenerateBullets(int amount)
    {
        for(int i=0; i<numberOfBullets; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, transform);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }

        return bulletPool;
    }

    public Bullet RequestBullet()
    {
        foreach(Bullet bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
                return bullet;
        }

        Bullet newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, transform);
        newBullet.gameObject.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;

    }
}
