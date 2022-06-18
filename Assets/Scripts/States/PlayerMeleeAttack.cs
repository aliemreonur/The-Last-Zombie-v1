using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : BaseState
{
    public PlayerMeleeAttack(PlayerSM stateMachine, Animator animator) : base("PlayerMeleeAttack", stateMachine, animator) { }

    public override void Enter()
    {
        animator.SetBool("isHitting", true);
        base.Enter();
    }


    public override void NormalUpdate()
    {
        Weapon.Instance.Fire();
        if (PlayerController.Instance.playerInput.Player.Fire.WasReleasedThisFrame())
        {
            if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                stateMachine.ChangeState(stateMachine.idleState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }

        //else if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() != Vector2.zero)
        //{
          //  stateMachine.ChangeState(stateMachine.runFireState);
        //}
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        animator.SetBool("isHitting", false);
        base.Exit();
    }
}
