using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeState : EnemyBaseState
{
    private EnemyStateManager _enemy;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered Range State");
        enemy.enemyAnimator.SetBool("isWalking", false);
        enemy.enemyAnimator.SetBool("RangeAttack", true);
        _enemy = enemy;
        enemy.GetComponent<NavMeshAgent>().speed = 0f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

    public override void EndAction(int i)
    {
        switch (i)
        {
            case 0:
                _enemy.EnemyController.SpawnProjectile();
                break;
            case 1:
                _enemy.EnemyController.LaunchProjectile();
                break;
            case 2:
                _enemy.enemyAnimator.SetBool("RangeAttack", false);
                _enemy.SwitchState(_enemy.ChaseState);
                break;
        }
    }
}
