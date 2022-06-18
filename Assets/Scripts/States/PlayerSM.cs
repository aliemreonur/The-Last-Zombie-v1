using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Running runningState;
    [HideInInspector] public Firing firingState;
    [HideInInspector] public RunningFiring runFireState;
    [HideInInspector] public PlayerMeleeAttack playerMeleeAttack;

    public Animator animator;
    BaseState currentState;

    private void Awake()
    {
        idleState = new Idle(this, animator);
        runningState = new Running(this, animator);
        firingState = new Firing(this, animator);
        runFireState = new RunningFiring(this, animator);
        playerMeleeAttack = new PlayerMeleeAttack(this, animator);
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
        if(!PlayerController.Instance.IsAlive)
        {
            animator.SetTrigger("isDead");
        }
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
}
