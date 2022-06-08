using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyState
{
    public EnemyPatrol(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _anim, EnemyStateMachine _enemyStateMachine) : base("EnemyPatrol", _zombie, _navMeshAgent, _anim, _enemyStateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void NormalUpdate()
    {
        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
