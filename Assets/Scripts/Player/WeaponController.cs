using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : Singleton<WeaponController>
{
    /// <summary>
    /// IMPORTANT: Weapon ID's need to be ordered accordingly on the Right Hand in the inspector.
    /// 
    /// </summary>
    #region Fields
    [SerializeField] ParticleSystem _fireMuzzleEffect;
    public WeaponType[] weaponTypes;
    public Action OnPlayerReload;
    public bool IsMelee => _isMelee;

    //Needs attention
    private WeaponType _currentWeapon; //might change this to a normal class 
    private bool _isMelee, _canShoot = true, _isReloading; 
    private int _maxAmmo, _currentAmmo;
    private int _currentWeaponId;
    private float _fireRate, _nextFire=0;
    private AudioSource _audioSource;
    private AudioClip _shotClip;
    private WaitForSeconds _reloadTime = new WaitForSeconds(2); //maybe this can also be pulled through the weapontype class
    #endregion

    #region StartGame
    private void OnEnable()
    {
        PlayerController.Instance.changeWeapon.performed += OnChangeWeapon;
        PlayerController.Instance.reload.performed += OnReload;

        _audioSource = GetComponent<AudioSource>();
        {
            if (_audioSource == null)
            {
                Debug.LogError("The audio source on the weapon is null");
            }
        }

    }
    void Start()
    {
        StartWeapon();
    }
    #endregion

    #region Reload
    private void OnReload(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.IsRunning)
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (((_currentAmmo < _maxAmmo)) && !_isReloading && !_isMelee)
        {
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

    IEnumerator ReloadRoutine()
    {
        yield return _reloadTime;
        _currentAmmo = _maxAmmo;
        UIManager.Instance.UpdateAmmoCount(_currentAmmo, _maxAmmo, false);
        _isReloading = false;
        _canShoot = true;
    }

    #endregion

    #region Action
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
                //Bullet bullet = PoolManager.Instance.RequestBullet(transform.position);
                GameObject bullet = PoolManager.Instance.RequestObject(1, transform.position);
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
    #endregion

    #region WeaponChange
    private void OnChangeWeapon(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (PlayerController.Instance.changeWeapon.IsPressed())
        {
            bool nextWeapon = _currentWeaponId + 1 < weaponTypes.Length ? true : false;

            if (nextWeapon)
            {
                _currentWeaponId++;
            }
            else
            {
                _currentWeaponId--;
            }
            ChangeWeapon(_currentWeaponId);
            if (!_currentWeapon.isMelee)
            {
                UIManager.Instance.UpdateAmmoCount(_currentAmmo, _maxAmmo, false);
            }
        }
    }

    private void StartWeapon()
    {
        _currentWeapon = weaponTypes[1];
        _currentWeapon.currentAmmo = _currentWeapon.maxAmmo;
        MatchWeaponValues();
    }

    private void ChangeWeapon(int id)
    {
        _currentWeapon.currentAmmo = _currentAmmo;
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
        MatchWeaponValues();
        UIManager.Instance.ChangeWeapon(_currentWeapon.name, _currentWeapon.isMelee);
    }

    private void MatchWeaponValues()
    {
        _isMelee = _currentWeapon.isMelee;
        _maxAmmo = _currentWeapon.maxAmmo;
        _fireRate = _currentWeapon.fireRate;
        _shotClip = _currentWeapon.shotClip;
        _currentAmmo = _currentWeapon.currentAmmo;
    }

    #endregion

}
