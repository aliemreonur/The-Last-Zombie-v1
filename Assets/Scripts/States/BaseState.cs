using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected PlayerSM stateMachine;
    protected Animator animator;

    public BaseState(string name, PlayerSM stateMachine, Animator animator)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    public virtual void Enter() { }
    public virtual void PhysicsUpdate() { }
    public virtual void NormalUpdate() { }
    public virtual void Exit() { }
}
