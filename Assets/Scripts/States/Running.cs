using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : BaseState
{
    private PlayerSM _playerSm;

    public Running(PlayerSM stateMachine, Animator animator) : base("Running", stateMachine, animator)
    {
        _playerSm = (PlayerSM)stateMachine;
    }

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
            stateMachine.ChangeState(_playerSm.idleState);
        }
        if (PlayerController.Instance.playerInput.Player.Fire.WasPressedThisFrame())
            stateMachine.ChangeState(_playerSm.runFireState);
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
