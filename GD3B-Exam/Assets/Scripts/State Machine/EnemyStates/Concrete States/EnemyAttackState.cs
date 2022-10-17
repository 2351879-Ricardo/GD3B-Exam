using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _timeSinceAttack;
    private Vector3 _enemyToPlayerVector3;
    private int _randNum;
    private EnemyStateManager _enemy;
    
    //Attack Initialization
    public override void EnterState(EnemyStateManager enemy)
    {
        for (int i = 1; i <= 3; i++)
        {
            enemy.enemyAnimator.SetBool($"break{i}", false);
        }
        
        _enemy = enemy;
        _randNum = Random.Range(1, 3);
        Debug.Log(_randNum);

        for (int i = 1; i <= _randNum; i++)
        {
            enemy.enemyAnimator.SetBool($"attack{i}", true);
        }
        enemy.enemyAnimator.SetBool($"break{_randNum}", true);
        
        _timeSinceAttack = 0f;
        // ENEMY ATTACK SPEED >> enemy.EnemyController.EnemySo.EnemyDamagePerAttack
    }

    //Updated every frame - frame by frame logic
    public override void UpdateState(EnemyStateManager enemy)
    {
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - enemy.PlayerGameObject.transform.position;
        if (_enemyToPlayerVector3.magnitude > enemy.EnemyController.EnemySo.EnemyAttackRange)
        {
            enemy.enemyAnimator.SetBool("isAttacking", false);
            enemy.SwitchState(enemy.ChaseState);
        }

        else
        {
            if (_timeSinceAttack >= enemy.EnemyController.EnemySo.EnemyAttackSpeed)
            {
                var randomNum = Random.Range(0f, 1f);
                if (randomNum <= enemy.EnemyController.EnemySo.EnemyHitChance01)
                {
                    //enemy.PlayerStats.TakeDamage(enemy.EnemyController.EnemySo.EnemyDamagePerAttack);
                    _timeSinceAttack = 0f;
                }
            }
            
            _timeSinceAttack += Time.deltaTime;
        }
    }


    public override void EndAction(int i)
    {
        _enemy.enemyAnimator.SetBool($"attack{i}", false);
    }
}
