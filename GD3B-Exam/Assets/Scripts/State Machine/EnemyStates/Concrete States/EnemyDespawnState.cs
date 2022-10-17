using UnityEngine;

public class EnemyDespawnState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        GameObject.Destroy(enemy.gameObject);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
