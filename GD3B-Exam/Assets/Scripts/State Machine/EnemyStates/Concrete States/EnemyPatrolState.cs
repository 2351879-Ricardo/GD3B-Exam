using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter Patrol State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("Update Patrol State");
    }
}