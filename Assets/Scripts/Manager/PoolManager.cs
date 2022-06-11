using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    [SerializeField] private Bullet _bullet;
    [SerializeField] private BloodEffect _bloodEffect;

    public List<Bullet> bulletPool = new List<Bullet>();
    public List<BloodEffect> bloodEffectPool = new List<BloodEffect>();

    private int numberOfBullets = 20;
    private int numberOfBlood = 20;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBullets(numberOfBullets);
        GenerateBloodEFfectPool(numberOfBlood);
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
}
