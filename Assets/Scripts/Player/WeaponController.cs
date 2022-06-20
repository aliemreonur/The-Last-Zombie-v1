using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : Singleton<WeaponController>
{
    /// <summary>
    /// IMPORTANT: Weapon ID's need to be ordered accordingly on the Right Hand in the inspector. 
    /// </summary>

    [SerializeField] ParticleSystem _fireMuzzleEffect;
    public WeaponType[] weaponTypes;
    public Action OnPlayerReload;
    public bool IsMelee => _isMelee;

    //Needs attention
    private WeaponType _currentWeapon; //will move this to a new class rather than a scriptable obj.
    private bool _isMelee, _canShoot = true, _isReloading; 
    private int _maxAmmo, _currentAmmo;
    private int _currentWeaponId;
    private float _fireRate, _nextFire=0;
    private AudioSource _audioSource;
    private AudioClip _shotClip;

    private WaitForSeconds _reloadTime = new WaitForSeconds(2); //maybe this can also be pulled through the weapontype class

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
        _currentWeaponId = 1;
        _currentWeapon.currentAmmo = _currentWeapon.maxAmmo;
        _currentAmmo = _currentWeapon.currentAmmo;
        PlayerController.Instance.changeWeapon.performed += ChangeWeapon_performed;
     
    }


    private void ChangeWeapon_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (PlayerController.Instance.changeWeapon.IsPressed())
        {
            bool nextWeapon = _currentWeaponId + 1 < weaponTypes.Length ? true : false;

            if(nextWeapon)
            {
                _currentWeaponId++;
            }
            else
            {
                _currentWeaponId--;
            }
            ChangeWeapon(_currentWeaponId);
            if(!_currentWeapon.isMelee)
            {
                UIManager.Instance.UpdateAmmoCount(_currentAmmo, _maxAmmo, false);
            }
        }

    }

    public void MeleeHit()
    {
        if (_currentWeapon.shotClip != null)
        {
            _audioSource.PlayOneShot(_currentWeapon.shotClip);
        }
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

    void Update()
    {
        Reload();
    }

    private void Reload()
    {
        if (((PlayerController.Instance.reload.IsPressed() && !_isReloading && _currentAmmo < _maxAmmo) || _currentAmmo <= 0) && !_isReloading && !_isMelee) //TOO MANY CONDITIONS!!
        {
            Debug.Log("Reload");
            //better to update to method callback system
            _isReloading = true;
            if (_currentWeapon.reloadClip != null)
            {
                _audioSource.PlayOneShot(_currentWeapon.reloadClip);
            }
            _canShoot = false;
            OnPlayerReload?.Invoke();
            StartCoroutine(ReloadRoutine());
        } 
    }

    private void ChangeWeapon(int id)
    {
        _currentWeapon = weaponTypes[id];
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
        _isMelee = _currentWeapon.isMelee;
        _maxAmmo = _currentWeapon.maxAmmo;
        _fireRate = _currentWeapon.fireRate;
        _shotClip = _currentWeapon.shotClip;
        _currentAmmo = _currentWeapon.currentAmmo;
        Debug.Log("Setted the current ammo to: " + _currentAmmo + "weapon ammo: " + _currentWeapon.currentAmmo);
        UIManager.Instance.ChangeWeapon(_currentWeapon.name, _currentWeapon.isMelee);
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
