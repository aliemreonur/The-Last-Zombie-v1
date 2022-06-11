using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDead : EnemyState
{
    public EnemyDead(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine) : base("EnemyDead", _zombie, _navMeshAgent, _animator, _enemyStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        navMeshAgent.isStopped = true;
        animator.SetTrigger("isDead");
        
    }

    public override void NormalUpdate()
    {
       
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {
        
    }
}
