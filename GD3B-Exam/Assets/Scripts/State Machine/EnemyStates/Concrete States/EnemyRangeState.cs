using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered Range State");
        enemy.enemyAnimator.SetBool("RangeAttack", true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

    public override void EndAction(int i)
    {
        
    }
}
