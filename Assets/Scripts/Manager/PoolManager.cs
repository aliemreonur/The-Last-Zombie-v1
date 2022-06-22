using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : Singleton<PoolManager>
{    
    [Serializable]
    public struct Pool
    {
        [HideInInspector] public List<GameObject> objectsToPool;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools; //Zombie, blood, bullet

    private void Awake()
    {
        base.Awake();

        for(int j = 0; j<pools.Length; j++)
        {
            pools[j].objectsToPool = new List<GameObject>();

            for(int i=0; i<pools[j].poolSize; i++)
            {
                GameObject spawnedObj = Instantiate(pools[j].objectPrefab, transform.position, Quaternion.identity, transform);
                spawnedObj.SetActive(false);

                pools[j].objectsToPool.Add(spawnedObj);
            }
        }
        
    }

    public GameObject RequestObject(int objectType, Vector3 pos)
    {
        foreach(GameObject obj in pools[objectType].objectsToPool)
        {
            if(!obj.gameObject.activeInHierarchy)
            {
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(pools[objectType].objectPrefab, transform.position, Quaternion.identity, transform);
        pools[objectType].objectsToPool.Add(newObj);
        newObj.transform.position = pos;

        return newObj;
    }


    /* 
    //DEPRECIATED CODE
    #region Fields
    [SerializeField] private Bullet _bullet;
    [SerializeField] private BloodEffect _bloodEffect;
    [SerializeField] private Zombie _zombie;

    public List<Bullet> bulletPool = new List<Bullet>();
    public List<Zombie> zombiePool = new List<Zombie>();
    public List<BloodEffect> bloodEffectPool = new List<BloodEffect>();

    private int _numberOfBullets = 20;
    private int _numberOfBlood = 20;
    private int _numberOfZombies = 50;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GenerateBullets(_numberOfBullets);
        GenerateBloodEFfectPool(_numberOfBlood);
        GenerateZombies(_numberOfZombies);
    }

    List<Bullet> GenerateBullets(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity, transform);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }

        return bulletPool;
    }

    List<BloodEffect> GenerateBloodEFfectPool(int amount)
    {
        for(int i =0; i<amount;i++)
        {
            BloodEffect bloodEffect = Instantiate(_bloodEffect, transform.position, Quaternion.identity, transform);
            bloodEffect.gameObject.SetActive(false);
            bloodEffectPool.Add(bloodEffect);
        }

        return bloodEffectPool;
    }

    List<Zombie> GenerateZombies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Zombie zombie = Instantiate(_zombie, transform.position, Quaternion.identity, transform);
            zombie.gameObject.SetActive(false);
            zombiePool.Add(zombie);
        }

        return zombiePool;
    }

    public Bullet RequestBullet(Vector3 bulletPos)
    {
        foreach(Bullet bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.transform.position = bulletPos;
                bullet.gameObject.SetActive(true);
                return bullet;
            }
                
        }

        Bullet newBullet = Instantiate(_bullet, transform.position, Quaternion.identity, transform);
        bulletPool.Add(newBullet);
        newBullet.transform.position = bulletPos;
        return newBullet;

    }

    public Zombie RequestZombie(Vector3 zombiePos)
    {
        foreach (Zombie zombie in zombiePool)
        {
            if (!zombie.gameObject.activeInHierarchy)
            {
                zombie.transform.position = zombiePos;
                zombie.gameObject.SetActive(true);
                return zombie;
            }

        }

        Zombie newZombie = Instantiate(_zombie, transform.position, Quaternion.identity, transform);
        zombiePool.Add(newZombie);
        newZombie.transform.position = zombiePos;
        return newZombie;
    }

    public BloodEffect RequestBloodEffect(Vector3 bloodPosition)
    {
        foreach(var bloodEffect in bloodEffectPool)
        {
            if(!bloodEffect.gameObject.activeInHierarchy)
            {
                bloodEffect.gameObject.SetActive(true);
                bloodEffect.transform.position = bloodPosition;
                return bloodEffect;
            }
        }

        BloodEffect additionalBlood = Instantiate(_bloodEffect, transform.position, Quaternion.identity, transform);
        bloodEffectPool.Add(additionalBlood);
        additionalBlood.transform.position = bloodPosition;
        return additionalBlood;
    }
    */
}
