using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private EnemyState currentEnemyState;
    private Zombie _zombie;

    [HideInInspector] public EnemyIdle enemyIdle;
    [HideInInspector] public EnemyPatrol enemyPatrol;
    [HideInInspector] public EnemyAttacking enemyAttacking;
    [HideInInspector] public EnemyHit enemyHit;
    [HideInInspector] public EnemyDead enemyDead;
    [HideInInspector] public EnemyChasePlayer enemyChasePlayer;

    private WaitForSeconds delayBeforePatrol = new WaitForSeconds(2f);

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        enemyIdle = new EnemyIdle(_zombie, _navMeshAgent, _animator, this);
        enemyPatrol = new EnemyPatrol(_zombie, _navMeshAgent, _animator, this);
        enemyAttacking = new EnemyAttacking(_zombie, _navMeshAgent, _animator, this);
        enemyHit = new EnemyHit(_zombie, _navMeshAgent, _animator, this);
        enemyDead = new EnemyDead(_zombie, _navMeshAgent, _animator, this);
        enemyChasePlayer = new EnemyChasePlayer(_zombie, _navMeshAgent, _animator, this);
        //is it possible to use generic here??
    }

    private void Start()
    {
        currentEnemyState = GetInitialState();
        if (currentEnemyState != null)
        {
            currentEnemyState.Enter();
        }
    }

    // Update is called once per frame
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

    public void Delay() //this method is for delaying a non-monobehaviour script, inheriting enemy states.
    {
        StartCoroutine(DelayForPatrolRoutine());
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //PoolManager.Instance.RequestBloodEffect(other.GetContact(0).point);
            BloodEffect bloodEffect= PoolManager.Instance.RequestBloodEffect(other.transform.position);
            bloodEffect.transform.parent = transform;
            other.gameObject.SetActive(false);
            _zombie.Damage();
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

    private void OnEnable()
    {
        EndLevel.OnSuccess += OnGameEnd;
    }

    private void OnGameEnd()
    {
        ChangeState(enemyIdle);
        _navMeshAgent.speed = 0;
    }

    private void OnDisable()
    {
        EndLevel.OnSuccess -= OnGameEnd;
    }

    IEnumerator DelayForPatrolRoutine()
    {
        yield return delayBeforePatrol;
    }

    private void OnGUI()
    {
        string content = currentEnemyState != null ? currentEnemyState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
