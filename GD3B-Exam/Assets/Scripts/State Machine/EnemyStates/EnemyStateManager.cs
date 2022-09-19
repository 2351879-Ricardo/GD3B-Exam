using System;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState _currentState;
    private EnemyAttackState _attackState = new EnemyAttackState();
    private EnemyChaseState _chaseState = new EnemyChaseState();
    private EnemyDodgeState _dodgeState = new EnemyDodgeState();
    private EnemyPatrolState _patrolState = new EnemyPatrolState();

    private void Start()
    {
        _currentState = _patrolState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    private void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
