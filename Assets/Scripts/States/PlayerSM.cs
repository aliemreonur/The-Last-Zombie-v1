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
        idleState = new Idle(this, animator);
        runningState = new Running(this, animator);
        firingState = new Firing(this, animator);
        runFireState = new RunningFiring(this, animator);
        //add the animator additional to this??
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }


}
