using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Running runningState;
    [HideInInspector] public Firing firingState;
    [HideInInspector] public RunningFiring runFireState;

    public Animator animator;
    BaseState currentState;

    private void Awake()
    {
        idleState = new Idle(this, animator);
        runningState = new Running(this, animator);
        firingState = new Firing(this, animator);
        runFireState = new RunningFiring(this, animator);
        //add the animator additional to this??
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
}
