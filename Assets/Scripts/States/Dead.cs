using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : BaseState
{
    public Dead(PlayerSM stateMachine, Animator animator) : base("Dead", stateMachine, animator) { }

    public override void Enter()
    {
        animator.SetTrigger("isDead");
    }

    public override void Exit()
    {
    }

    public override void NormalUpdate()
    { 
    }

    public override void PhysicsUpdate()
    {
        
    }

}
