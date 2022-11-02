using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered Charge State");
        enemy.SwitchState(enemy.ChaseState);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

    public override void EndAction(int i)
    {
        
    }
}
