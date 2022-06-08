using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : StateMachine
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Running runningState;
    [HideInInspector] public Firing firingState;
    [HideInInspector] public RunningFiring runFireState;

    public Animator animator;

    private void Awake()
    {
        idleState = new Idle(this);
        runningState = new Running(this);
        firingState = new Firing(this);
        runFireState = new RunningFiring(this);
        //add the animator additional to this??
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }


}
