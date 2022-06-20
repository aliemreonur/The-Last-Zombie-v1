using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{

    [HideInInspector] public EnemyIdle enemyIdle;
    [HideInInspector] public EnemyPatrol enemyPatrol;
    [HideInInspector] public EnemyAttacking enemyAttacking;
    [HideInInspector] public EnemyHit enemyHit;
    [HideInInspector] public EnemyDead enemyDead;
    [HideInInspector] public EnemyChasePlayer enemyChasePlayer;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private EnemyState currentEnemyState;
    private Zombie _zombie;

    void Awake()
    {
   
        Initialize();
        enemyIdle = new EnemyIdle(_zombie, _navMeshAgent, _animator, this);
        enemyPatrol = new EnemyPatrol(_zombie, _navMeshAgent, _animator, this);
        enemyAttacking = new EnemyAttacking(_zombie, _navMeshAgent, _animator, this);
        enemyHit = new EnemyHit(_zombie, _navMeshAgent, _animator, this);
        enemyDead = new EnemyDead(_zombie, _navMeshAgent, _animator, this);
        enemyChasePlayer = new EnemyChasePlayer(_zombie, _navMeshAgent, _animator, this);
    }

    private void Start()
    {
        currentEnemyState = GetInitialState();
        if (currentEnemyState != null)
        {
            currentEnemyState.Enter();
        }
    }

    void Update()
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.NormalUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.PhysicsUpdate();
        }
    }

    public void ChangeState(EnemyState nextState)
    {
        currentEnemyState.Exit();
        currentEnemyState = nextState;
        currentEnemyState.Enter();
    }

    protected EnemyState GetInitialState()
    {
        return enemyIdle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && _zombie.IsAlive)
        {
            BloodEffect bloodEffect = PoolManager.Instance.RequestBloodEffect(other.transform.position);
            if(bloodEffect != null)
            {
                bloodEffect.transform.parent = transform;
                other.gameObject.SetActive(false);
            }
            _zombie.Damage();
        }

        else if (other.gameObject.CompareTag("Bat"))
        {
            _zombie.Damage(true);
        }
    }

    private void Initialize()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent of the zombie is null");
        }
        _navMeshAgent.speed = 1.5f; //this will be pulled through the hardness level
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The animator of the zombie is null");
        }
        _zombie = GetComponent<Zombie>();
        if (_zombie == null)
        {
            Debug.LogError("The Enemystate could not get its zombie script");
        }
    }

    private void OnGameEnd()
    {
        ChangeState(enemyIdle);
        _navMeshAgent.speed = 0;
    }

    private void OnEnable()
    {
        EndLevel.OnZombieReachedEndPoint += OnGameEnd;
        PlayerController.Instance.OnPlayerDeath += OnGameEnd;
    }

    private void OnDisable()
    {
        EndLevel.OnZombieReachedEndPoint -= OnGameEnd;
        PlayerController.Instance.OnPlayerDeath -= OnGameEnd;
    }
}
