using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _timeSinceAttack;
    private Vector3 _enemyToPlayerVector3;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        _timeSinceAttack = 0f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - enemy.Player.gameObject.transform.position;
        if (_enemyToPlayerVector3.magnitude > enemy.EnemyController.EnemySo.EnemyAttackRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        else
        {
            if (_timeSinceAttack >= enemy.EnemyController.EnemySo.EnemyAttackSpeed)
            {
                var randomNum = Random.Range(0f, 1f);
                if (randomNum <= enemy.EnemyController.EnemySo.EnemyDamagePerAttack)
                {
                    // HIT PLAYER
                }
            }
            
            _timeSinceAttack += Time.deltaTime;
        }
    }
}
