using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : Singleton<Weapon>
{
    /// <summary>
    /// IMPORTANT: Weapon ID's need to be ordered accordingly on the Right Hand in the inspector. 
    /// </summary>
    
    public WeaponType[] weaponTypes;
    [SerializeField] ParticleSystem _fireMuzzleEffect;

    public Action OnPlayerReload;
    public bool IsMelee => _isMelee;

    private WeaponType currentWeapon;
    private bool _isMelee, _canShoot = true, _isReloading; 
    private int _maxAmmo, _currentAmmo;
    private float _fireRate, _nextFire=0;
    private AudioSource _audioSource;
    private AudioClip _shotClip;

    private WaitForSeconds _reloadTime = new WaitForSeconds(2); //maybe this can also be pulled through the weapontype class

    private bool justStarted = true;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        {
            if(_audioSource == null)
            {
                Debug.LogError("The audio source on the weapon is null");
            }
        }
        ChangeWeapon(1); //start with the gun!
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        justStarted = false;
    }

    void Update()
    {
        //use the new input system here!
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        Reload();
    }

    public void Fire()
    {
        if (Time.time > _nextFire && _currentAmmo > 0 && _canShoot)
        {
            if (!_isMelee)
            {
                if (_fireMuzzleEffect != null)
                {
                    _fireMuzzleEffect.Play();
                }
                if (_shotClip != null)
                {
                    _audioSource.PlayOneShot(_shotClip);
                }
                Bullet bullet = PoolManager.Instance.RequestBullet(transform.position);
                bullet.transform.rotation = PlayerController.Instance.transform.rotation; //may be changed to gunpos rotation later on
        
                _currentAmmo--;
                UIManager.Instance.UpdateAmmoCount(_currentAmmo, _maxAmmo, _currentAmmo <= 0);
                if (_currentAmmo <= 0 && !_isReloading)
                {      
                    Reload();
                }
            }
            _nextFire = Time.time + _fireRate;
        }
    }

    private void Reload()
    {
        if (((Input.GetKeyDown(KeyCode.R) && !_isReloading && _currentAmmo < _maxAmmo) || _currentAmmo <= 0) && !_isReloading && !_isMelee) //TOO MANY CONDITIONS!!
        {
            _isReloading = true;
            if (currentWeapon.reloadClip != null)
            {
                _audioSource.PlayOneShot(currentWeapon.reloadClip);
            }
            //use the new input system
            _canShoot = false;
            OnPlayerReload?.Invoke();
            StartCoroutine(ReloadRoutine());
            
        }
        //Access the reloading fill image
        //the coroutine image fill according to reload time
        //On screen Reloading text flashing on and of
        //only return hasreloaded to true if the reload has finished. 
    }

    public void ChangeWeapon(int id)
    {
        if(!justStarted)
        {
            currentWeapon.currentAmmo = _currentAmmo;
        }

        currentWeapon = weaponTypes[id];
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == id)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        _isMelee = currentWeapon.isMelee;
        _maxAmmo = currentWeapon.maxAmmo;
        _fireRate = currentWeapon.fireRate;
        _shotClip = currentWeapon.shotClip;
        _currentAmmo = currentWeapon.currentAmmo; ; 
        UIManager.Instance.ChangeWeapon(currentWeapon.name, currentWeapon.isMelee);
    }

    public void MeleeHit()
    {
        if(currentWeapon.shotClip != null)
        {
            _audioSource.PlayOneShot(currentWeapon.shotClip);
        }
    }

    IEnumerator ReloadRoutine()
    {
        yield return _reloadTime;
        _currentAmmo = _maxAmmo;
        UIManager.Instance.UpdateAmmoCount(_currentAmmo, _maxAmmo, false);
        _isReloading = false;
        _canShoot = true;
    }
}
