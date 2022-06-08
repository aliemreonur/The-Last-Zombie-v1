using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHit : EnemyState
{
    public EnemyHit(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _anim, EnemyStateMachine _enemyStateMachine) : base("EnemyHit", _zombie, _navMeshAgent, _anim, _enemyStateMachine)
    { }

    public override void Enter()
    {
        animator.SetTrigger("isHit");
        base.Enter();
    }

    public override void NormalUpdate()
    {
        if(!zombie.IsHit)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyIdle);
        }
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        animator.ResetTrigger("isHit");
        base.Exit();
    }
}
