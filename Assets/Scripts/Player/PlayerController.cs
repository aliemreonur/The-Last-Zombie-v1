using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] private float _speed = 3f;
    [SerializeField] int _playerHealth = 100;

    public PlayerInputActions playerInput;
    public InputAction move, rotate;

    public Action OnPlayerDeath;

    private float _initialSpeed; //this is for referencing the speed variable if it is changed via inspector.
    private Vector3 moveVector, lookVector = Vector3.zero;
    private WaitForSeconds playerHitSlowDown = new WaitForSeconds(2f);

    public bool IsAlive
    {
        get { return _isAlive; }
    }

    public bool IsHit
    {
        get
        {
            return _isHit;
        }
        set
        {
            _isHit = value;
        }
    }

    private bool _isHit;
    private bool _isAlive = true;

    public override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInputActions();
        _initialSpeed = _speed;
    }


    void FixedUpdate()
    {
        Move();
        LookAt();

        if(playerInput.Player.Reload.IsPressed())
        {
            Debug.Log("reload!");
        }
    }

    public void Damage(int damageAmount)
    {
        _isHit = true;
        if(_isHit)
        {
            StartCoroutine(PlayerHitRoutine());
        }
        _playerHealth -= damageAmount;
        UIManager.Instance.UpdatePlayerHealth(_playerHealth);
        if (_playerHealth <= 0 && _isAlive)
        {
            _isAlive = false;
            OnPlayerDeath?.Invoke();
        }
    }


    private void Move()
    {
        if (move.ReadValue<Vector2>() != Vector2.zero)
        {
            moveVector = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);

            //Rigidbody addforce was changed 
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

    IEnumerator PlayerHitRoutine()
    {
        _isHit = false;
        _speed = _initialSpeed / 4;
        yield return playerHitSlowDown;
        _speed = _initialSpeed;
    }
}
