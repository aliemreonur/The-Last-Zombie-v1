using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Firing : BaseState
{
    public Firing(PlayerSM stateMachine, Animator animator) : base("Firing", stateMachine, animator){}

    public override void Enter()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShooting", true);
        base.Enter();
    }

    public override void NormalUpdate()
    {
        PlayerController.Instance.Fire();
        if(PlayerController.Instance.playerInput.Player.Fire.WasReleasedThisFrame())
        {
            if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                stateMachine.ChangeState(stateMachine.idleState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }

        else if(PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.runFireState);
        }
        
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
