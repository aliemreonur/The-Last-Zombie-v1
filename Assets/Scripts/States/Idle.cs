using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    public Idle(PlayerSM stateMachine, Animator animator) : base("Idle", stateMachine, animator){}

    public override void Enter()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShooting", false);
        animator.SetBool("isHitting", false);
        base.Enter();
    }

    public override void NormalUpdate()
    {
        if ((PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>()).sqrMagnitude >float.Epsilon)
            stateMachine.ChangeState(stateMachine.runningState);
        if(PlayerController.Instance.playerInput.Player.Fire.WasPressedThisFrame())
        {
            if(WeaponController.Instance.IsMelee)
            {
                stateMachine.ChangeState(stateMachine.playerMeleeAttack);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.firingState);
            }

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
