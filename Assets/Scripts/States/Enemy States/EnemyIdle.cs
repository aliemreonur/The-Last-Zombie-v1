using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine) : base("EnemyIdle", _zombie, _navMeshAgent, _animator, _enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void NormalUpdate()
    {
        if(zombie.IsAttacking)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyAttacking);
        }
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        if(zombie.IsHit)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyHit);
        }
        base.PhysicsUpdate();
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}