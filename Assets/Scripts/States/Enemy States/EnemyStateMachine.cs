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


    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        enemyIdle = new EnemyIdle(_zombie, _navMeshAgent, _animator, this);
        enemyPatrol = new EnemyPatrol(_zombie, _navMeshAgent, _animator, this);
        enemyAttacking = new EnemyAttacking(_zombie, _navMeshAgent, _animator, this);
        enemyHit = new EnemyHit(_zombie, _navMeshAgent, _animator, this);
        //is it possible to use generic here??
    }

    private void Start()
    {
        currentEnemyState = GetInitialState();
        if (currentEnemyState != null)
        {
            currentEnemyState.Enter();
        }

        _zombie.Damage();
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Decrease the zombie health");
        }
    }

    private void Initialize()
    {
        //TOO MANY GET COMPONENTS WITH MANY ZOMBIES! 
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent of the zombie is null");
        }
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

    private void OnGUI()
    {
        string content = currentEnemyState != null ? currentEnemyState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
