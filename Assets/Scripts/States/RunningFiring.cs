using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningFiring : BaseState
{
    public RunningFiring(PlayerSM stateMachine, Animator animator) : base("RunningFiring", stateMachine, animator) {}

    public override void Enter()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isShooting", true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void NormalUpdate()
    {
        WeaponController.Instance.Fire();
        if (PlayerController.Instance.playerInput.Player.Fire.WasReleasedThisFrame())
        {
            if ((PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>().sqrMagnitude) <= float.Epsilon)
                stateMachine.ChangeState(stateMachine.idleState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }

        else if ((PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>().sqrMagnitude) <= float.Epsilon)
        {
            stateMachine.ChangeState(stateMachine.firingState);
        }

        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
