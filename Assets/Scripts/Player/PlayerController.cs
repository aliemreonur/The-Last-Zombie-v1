using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] ParticleSystem _fireMuzzleEffect;
    [SerializeField] Bullet _bullet;
    [SerializeField] private float _speed;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private Transform _gunPos;
    //[SerializeField] private int _playerHealth;
    [SerializeField] private int _clipSize = 14; //this will be updated with the weapons class - according to the weapon holding!
    [SerializeField] private AudioClip _gunShotEffect, _reloadEffect;

    public PlayerInputActions playerInput;
    public InputAction move, rotate;

    public Action OnPlayerReload;


    private AudioSource _audioSource;
    private bool _isReloading;
    private bool _canShoot = true;
    private float _nextFire = 0;
    private int _currentAmmo;
    private Vector3 moveVector, lookVector = Vector3.zero;
    private WaitForSeconds _reloadTime = new WaitForSeconds(2);

    public bool IsAlive
    {
        get { return _isAlive; }
    }

    private bool _isAlive = true;

    public override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("The audio source of the palyer is null");
        }
        playerInput = new PlayerInputActions();
        _currentAmmo = _clipSize;
    }


    void FixedUpdate()
    {
        Move();
        //LookAt();
        Reload();
    }

    public void Fire()
    {
        if (Time.time > _nextFire && _currentAmmo>0 && _canShoot)
        {
            if(_fireMuzzleEffect != null)
            {
                _fireMuzzleEffect.Play();
            }
            if(_gunShotEffect != null)
            {
                _audioSource.PlayOneShot(_gunShotEffect);
            }
            Bullet bullet = PoolManager.Instance.RequestBullet(transform.position + Vector3.up);
            bullet.transform.rotation = _gunPos.transform.rotation;
            _nextFire = Time.time + _fireRate;
            _currentAmmo--;
            UIManager.Instance.UpdateAmmoCount(_currentAmmo, _clipSize, _currentAmmo <= 0);
            if (_currentAmmo <=0)
            {
                _isReloading = true;
            }
        }
    }

    private void Move()
    {
        if (move.ReadValue<Vector2>() != Vector2.zero)
        {
            moveVector = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);

            transform.Translate(moveVector * _speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(moveVector, Vector3.up);
        }
    }

    private void LookAt()
    {
        if(rotate.ReadValue<Vector2>() != Vector2.zero)
        {
            lookVector = new Vector3(rotate.ReadValue<Vector2>().x, 0, rotate.ReadValue<Vector2>().y);
            transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
        }
        
    }

    private void Reload()
    {
        if((Input.GetKeyDown(KeyCode.R) || _isReloading) && _currentAmmo < _clipSize)
        {
            if(_reloadEffect != null)
            {
                _audioSource.PlayOneShot(_reloadEffect);
            }
            //use the new input system
            _canShoot = false;
            _isReloading = true;
            OnPlayerReload?.Invoke();
            StartCoroutine(ReloadRoutine());
            _isReloading = false;
        }
        //Access the reloading fill image
        //the coroutine image fill according to reload time
        //On screen Reloading text flashing on and of
        //only return hasreloaded to true if the reload has finished. 
    }


    private void OnEnable()
    {
        playerInput.Player.Enable();
        move = playerInput.Player.Move;
        rotate = playerInput.Player.Rotate;
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    IEnumerator ReloadRoutine()
    {
        yield return _reloadTime;
        _currentAmmo = _clipSize;
        UIManager.Instance.UpdateAmmoCount(_currentAmmo, _clipSize, false);
        _canShoot = true;
    }
}
