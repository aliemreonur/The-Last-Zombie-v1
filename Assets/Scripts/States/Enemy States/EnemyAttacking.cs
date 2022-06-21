using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacking : EnemyState
{
    public EnemyAttacking(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine) : base("EnemyAttacking", _zombie, _navMeshAgent, _animator, _enemyStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        zombie.IsAttacking = true;
        animator.SetBool("isAttacking", true);
    }

    public override void NormalUpdate()
    {
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        if(zombie.DistanceToPlayer() > 2.25f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyChasePlayer);
        }

        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        zombie.IsAttacking = false;
        animator.SetBool("isAttacking", false);
        base.Exit();
    }

}
