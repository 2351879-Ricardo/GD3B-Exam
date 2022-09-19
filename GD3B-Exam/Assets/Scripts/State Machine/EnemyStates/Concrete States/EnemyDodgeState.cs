using UnityEngine;

public class EnemyDodgeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter Dodge State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Update Dodge State");
    }
}