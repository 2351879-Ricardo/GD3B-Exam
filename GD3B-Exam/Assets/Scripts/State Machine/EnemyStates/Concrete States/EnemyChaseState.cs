using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private Vector3 _enemyToPlayerVector3;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.enemyAnimator.SetBool("isWalking", true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        var playerPos = enemy.PlayerGameObject.transform.position;
        enemy.EnemyNavMeshAgent.SetDestination(playerPos);
        
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - playerPos;
        if (_enemyToPlayerVector3.magnitude <= enemy.EnemyController.EnemySo.EnemyAttackRange && enemy.enemyAnimator)
        {
            enemy.SwitchState(enemy.AttackState);
            enemy.enemyAnimator.SetBool("isWalking", false);
        }
    }

    public override void EndAction(int i)
    {
        //Doing a thing
    }
}