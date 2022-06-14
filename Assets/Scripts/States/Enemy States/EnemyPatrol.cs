using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyState
{
    public EnemyPatrol(Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _anim, EnemyStateMachine _enemyStateMachine) : base("EnemyPatrol", _zombie, _navMeshAgent, _anim, _enemyStateMachine)
    { }

    private float _initialSpeed;
    private bool isMoving = false;
    private Vector3 pointToMove;

    public override void Enter()
    {
        _initialSpeed = navMeshAgent.speed;
        animator.SetBool("isPatroling", true);
        navMeshAgent.speed = Random.Range(0, 0.6f);
        base.Enter();
    }

    public override void Exit()
    {
        navMeshAgent.speed = _initialSpeed;
        animator.SetBool("isPatroling", false);
        base.Exit();
    }

    public override void NormalUpdate()
    {
        Patrol();
        if (zombie.IsAlerted)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyChasePlayer);
        }

        base.NormalUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Patrol()
    {
        if(!isMoving)
        {
            pointToMove = new Vector3(Random.Range(SpawnManager.Instance.posXLeftEdge, SpawnManager.Instance.posXRightEdge), 0, Random.Range(SpawnManager.Instance.posZStart, SpawnManager.Instance.posZEnd));
            isMoving = true;
        }

        navMeshAgent.SetDestination(pointToMove);
        if(navMeshAgent.remainingDistance <1.5f)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyIdle);
        }


        //assign a random point to go
        //idle for some time if reached to the point or to an end point.
        //reassign a random point
        //...
    }

}
