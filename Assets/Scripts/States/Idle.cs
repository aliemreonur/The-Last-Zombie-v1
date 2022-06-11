using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    PlayerSM _playerSm;

    public Idle(PlayerSM stateMachine, Animator animator) : base("Idle", stateMachine, animator)
    {
        _playerSm = (PlayerSM)stateMachine;
    }

    public override void Enter()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isShooting", false);
        base.Enter();
    }

    public override void NormalUpdate()
    {
        //this is with too many dots???
        if (PlayerController.Instance.playerInput.Player.Move.ReadValue<Vector2>() != Vector2.zero)
            stateMachine.ChangeState(_playerSm.runningState);
        if(PlayerController.Instance.playerInput.Player.Fire.WasPressedThisFrame())
            stateMachine.ChangeState(_playerSm.firingState);
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
