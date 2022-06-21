using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] private float _speed = 3f;
    [SerializeField] int _playerHealth = 100;

    public PlayerInputActions playerInput;
    public InputAction move, reload, changeWeapon;

    private float _initialSpeed;
    private Vector3 _moveVector;
    private WaitForSeconds _playerHitSlowDown = new WaitForSeconds(2f);
    private bool _isHit;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private bool _isAlive = true;

    #region Properties
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
    #endregion

    #region Methods
    public override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInputActions();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.LogError("The navmesh agent is null");
        }
        _initialSpeed = _speed;
    }

    public void Damage(int damageAmount)
    {
        _isHit = true;
        if (_isHit)
        {
            StartCoroutine(PlayerHitRoutine());
        }
        _playerHealth -= damageAmount;
        UIManager.Instance.UpdatePlayerHealth(_playerHealth);
        if (_playerHealth <= 0 && _isAlive)
        {
            _isAlive = false;
            GameManager.Instance.EndGame(false);
        }
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.IsRunning)
            Move();
    }

    private void Move()
    {
        if (move.ReadValue<Vector2>() != Vector2.zero && _isAlive)
        {
            _moveVector = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
            _navMeshAgent.Move(_moveVector * _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(_moveVector, Vector3.up);
        }
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        move = playerInput.Player.Move;
        reload = playerInput.Player.Reload;
        changeWeapon = playerInput.Player.ChangeWeapon;
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    IEnumerator PlayerHitRoutine()
    {
        _isHit = false;
        _speed = _initialSpeed / 4;
        yield return _playerHitSlowDown;
        _speed = _initialSpeed;
    }

    #endregion
}
