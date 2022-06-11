using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState 
{
    public string name;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    protected Zombie zombie;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(string name, Zombie _zombie, NavMeshAgent _navMeshAgent, Animator _animator, EnemyStateMachine _enemyStateMachine)
    {
        this.zombie = _zombie;
        this.name = name;
        this.navMeshAgent = _navMeshAgent;
        this.animator = _animator;
        this.enemyStateMachine = _enemyStateMachine;
    }

    public virtual void Enter() { }
    public virtual void NormalUpdate() { }
    public virtual void PhysicsUpdate()
    {
        if(!zombie.IsAlive)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyDead);
        }
    }
    public virtual void Exit() { }
}
