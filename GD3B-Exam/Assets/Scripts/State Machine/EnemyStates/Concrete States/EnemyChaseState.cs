using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter Chase State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Update Chase State");
    }
}