using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Firing : BaseState
{
    PlayerSM _playerSm;

    public Firing(PlayerSM stateMachine) : base("Firing", stateMachine)
    {
        _playerSm = (PlayerSM)stateMachine;
    }

    public override void Enter()
    {
        _playerSm.animator.SetBool("isRunning", false);
        _playerSm.animator.SetBool("isShooting", true);
        base.Enter();
    }

    public override void NormalUpdate()
    {
        PlayerController.Instance.Fire();
        if(PlayerController.Instance.playerInput.Player.Fire.WasReleasedThisFrame())
        {
            if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                stateMachine.ChangeState(_playerSm.idleState);
            else
                stateMachine.ChangeState(_playerSm.runningState);
        }

        else if(PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() != Vector2.zero)
        {
            stateMachine.ChangeState(_playerSm.runFireState);
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
