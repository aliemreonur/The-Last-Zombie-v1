using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasePlayer : EnemyState
{
    public EnemyChasePlayer(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine) : base("EnemyChasing", _zombie, _navMeshAgent, _animator, _enemyStateMachine)
    {
    }

    public override void Enter()
    {
        animator.SetBool("chasingPlayer", true);
        navMeshAgent.isStopped = false;
        base.Enter();
    }

    public override void NormalUpdate()
    {
        if(!zombie.IsAttacking)
        {
            navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
        }


        if (zombie.DistanceToPlayer() < 1.5f)
        {
            //Need a delay here before swwitching to attack state - a bug that makes the zombie move while attacking.
            enemyStateMachine.ChangeState(enemyStateMachine.enemyAttacking);
        }
        base.NormalUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void Exit()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("chasingPlayer", false);
        base.Exit();
    }
}
