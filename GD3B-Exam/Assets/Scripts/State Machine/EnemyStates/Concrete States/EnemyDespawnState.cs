using UnityEngine;

public class EnemyDespawnState : EnemyBaseState
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
