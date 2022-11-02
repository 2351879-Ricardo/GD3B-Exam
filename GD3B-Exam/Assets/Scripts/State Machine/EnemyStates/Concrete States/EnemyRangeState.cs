using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Object.Destroy(enemy.gameObject);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

    public override void EndAction(int i)
    {
        
    }
}
