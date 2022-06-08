using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningFiring : BaseState
{
    PlayerSM _playerSm;

    public RunningFiring(PlayerSM stateMachine) : base("RunningFiring", stateMachine)
    {
        _playerSm = (PlayerSM)stateMachine;
    }

    public override void Enter()
    {
        _playerSm.animator.SetBool("isRunning", true);
        _playerSm.animator.SetBool("isShooting", true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void NormalUpdate()
    {
        PlayerController.Instance.Fire();
        if (PlayerController.Instance.playerInput.Player.Fire.WasReleasedThisFrame())
        {
            if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                stateMachine.ChangeState(_playerSm.idleState);
            else
                stateMachine.ChangeState(_playerSm.runningState);
        }

        else if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
        {
            stateMachine.ChangeState(_playerSm.firingState);
        }

        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
