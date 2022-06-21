using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Running runningState;
    [HideInInspector] public Firing firingState;
    [HideInInspector] public RunningFiring runFireState;
    [HideInInspector] public PlayerMeleeAttack playerMeleeAttack;
    [HideInInspector] public Dead playerDead;

    public Animator animator;
    BaseState currentState;

    private void Awake()
    {
        idleState = new Idle(this, animator);
        runningState = new Running(this, animator);
        firingState = new Firing(this, animator);
        runFireState = new RunningFiring(this, animator);
        playerMeleeAttack = new PlayerMeleeAttack(this, animator);
        playerDead = new Dead(this, animator);
    }


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.NormalUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.PhysicsUpdate();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public BaseState GetInitialState()
    {
        return idleState;
    }

    private void EndGame()
    {
        if(PlayerController.Instance.IsAlive)
        {
            ChangeState(idleState);
        }
        else
        {
            ChangeState(playerDead);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= EndGame;
    }


}
