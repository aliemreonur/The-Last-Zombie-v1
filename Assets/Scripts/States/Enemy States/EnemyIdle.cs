using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine) : base("EnemyIdle", _zombie, _navMeshAgent, _animator, _enemyStateMachine)
    {}

    //bool willPatrol;

    public override void Enter()
    {
        //float patrolDecider = Random.Range(0, 100);
        //willPatrol = patrolDecider > 50 ? true : false; //changed on the new wave system.
        base.Enter();
        Patrol();
    }

    public override void NormalUpdate()
    {

        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        if(zombie.IsHit)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyHit);
        }

        if(zombie.IsAlerted)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyChasePlayer);
        }
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Patrol()
    {
        enemyStateMachine.ChangeState(enemyStateMachine.enemyPatrol);
    }
}
