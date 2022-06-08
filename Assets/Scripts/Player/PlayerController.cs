using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _rotationSensitivity;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private int _playerHealth;

    public PlayerInputActions playerInput;
    public InputAction move;
    private Rigidbody _rigidbody;

    public Action OnPlayerDeath;

    [SerializeField] ParticleSystem _fireMuzzleEffect;
    [SerializeField] Bullet _bullet;

    private float _nextFire = 0;
    Vector3 moveVector = Vector3.zero;

    public bool IsAlive
    {
        get { return _isAlive; }
    }
    private bool _isAlive = true;

    public override void Awake()
    {
        base.Awake();
        InitInputActions();
    }


    void FixedUpdate()
    {
        Move();
        LookRotation();
    }

    private void InitInputActions()
    {
        playerInput = new PlayerInputActions();
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) Debug.LogError("The rigidbody of the player is null");
    }

    private void Move()
    {
        moveVector += new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);

        _rigidbody.AddForce(moveVector, ForceMode.Impulse);
        moveVector = Vector3.zero;

        if (_rigidbody.velocity.y < 0f)
            _rigidbody.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
    }

    private void LookRotation()
    {
        Vector3 direction = _rigidbody.velocity;
        direction.y = 0;

        if (move.ReadValue<Vector2>() != Vector2.zero && direction != Vector3.zero)
        {
            _rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
            _rigidbody.angularVelocity = Vector3.zero;
    }

    public void Fire()
    {
        if(Time.time > _nextFire)
        {
            Bullet bullet = PoolManager.Instance.RequestBullet();
            bullet.transform.position = transform.position + Vector3.up;
            bullet.transform.rotation = transform.rotation;
            _fireMuzzleEffect.Play();
            bullet.gameObject.SetActive(true);
            _nextFire = Time.time + _fireRate;
        }
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        move = playerInput.Player.Move;
        playerInput.Player.Jump.started += OnJump;

    }


    private void OnJump(InputAction.CallbackContext obj)
    {
        if(IsGrounded())
        {
            moveVector += Vector3.up * _jumpHeight;
        }
    }

    private bool IsGrounded()
    {
        Ray checkGroundRay = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        RaycastHit hitInfo;
        if (Physics.Raycast(checkGroundRay, out hitInfo, 0.3f)) //can check tags for multiple surfaces.
            return true;
        else
            return false;
    }

    public void Damage()
    {
        _playerHealth--;
        if(_playerHealth<=0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void OnDisable()
    {
        playerInput.Player.Jump.started -= OnJump;
        playerInput.Player.Disable();

    }
}
