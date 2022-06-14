using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : BaseState
{
    public Running(PlayerSM stateMachine, Animator animator) : base("Running", stateMachine, animator){}

    public override void Enter()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isShooting", false);
        base.Enter();
    }

    public override void NormalUpdate()
    {
        if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        if (PlayerController.Instance.playerInput.Player.Fire.WasPressedThisFrame())
            stateMachine.ChangeState(stateMachine.runFireState);
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
