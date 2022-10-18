using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private float _timeSinceAttack, _attackRange;
    private Vector3 _enemyToPlayerVector3;
    private int _randNum;
    private EnemyStateManager _enemy;

    private int _layerMask;
    private bool _attacking = false;

    //Attack Initialization
    public override void EnterState(EnemyStateManager enemy)
    {
        _layerMask = 1 << 9;
        
        for (int i = 1; i <= 3; i++)
        {
            enemy.enemyAnimator.SetBool($"break{i}", false);
        }
        
        _enemy = enemy;
        _randNum = Random.Range(1, 4);
        Debug.Log(_randNum);

        for (int i = 1; i <= _randNum; i++)
        {
            enemy.enemyAnimator.SetBool($"attack{i}", true);
        }
        enemy.enemyAnimator.SetBool($"break{_randNum}", true);
        _attacking = true;
        
        _timeSinceAttack = 0f;
        _attackRange = enemy.EnemyController.EnemySo.EnemyAttackRange;
        
        
        // ENEMY ATTACK SPEED >> enemy.EnemyController.EnemySo.EnemyDamagePerAttack
        enemy.enemyAnimator.SetFloat("attackSpeed", enemy.EnemyController.EnemySo.EnemyAttackSpeed);
        
        //Lock Enemy in Place when attacking ----- Segway into more extravagent animations???
        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
    }

    //Updated every frame - frame by frame logic
    public override void UpdateState(EnemyStateManager enemy)
    {
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - enemy.PlayerGameObject.transform.position;
        var dist = _enemyToPlayerVector3.magnitude;
        var inRange = dist < enemy.EnemyController.EnemySo.EnemyAttackRange;
        
        if (inRange &&!_attacking)
        {
            Debug.Log("New Combo");
            EnterState(enemy);
        }

        else if (!inRange && !_attacking)
        {
            Debug.Log("Cant Fuckin Reach Him with My Tiny Arms");
            enemy.SwitchState(enemy.ChaseState);
        }
        
        /*

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
        }*/
    }


    public override void EndAction(int i)
    {
        _enemy.enemyAnimator.SetBool($"attack{i}", false);
        if (i == _randNum)
        {
            _attacking = false;
        }
    }
}
