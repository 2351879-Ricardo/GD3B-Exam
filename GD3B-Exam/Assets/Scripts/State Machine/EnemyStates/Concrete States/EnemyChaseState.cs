using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private Vector3 _enemyToPlayerVector3;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - enemy.Player.gameObject.transform.position;
        // Move Enemy to Player => NavMesh
        if (_enemyToPlayerVector3.magnitude <= enemy.EnemyController.EnemySo.EnemyAttackRange)
        {
            enemy.SwitchState(enemy.AttackState);
        }
    }
}