using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private float _timeSinceAttack, _attackRange;
    private Vector3 _enemyToPlayerVector3;
    private int _randNum;
    private EnemyStateManager _enemy;
    
    private bool _attacking = false;
    private RangeCheck _rc;

    //Attack Initialization
    public override void EnterState(EnemyStateManager enemy)
    {
        _rc = enemy.GetComponent<RangeCheck>();

        for (int i = 1; i <= 3; i++)
        {
            enemy.enemyAnimator.SetBool($"break{i}", false);
        }
        
        _enemy = enemy;
        _randNum = Random.Range(1, 4);

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
        
        //Lock Enemy in Place when attacking ----- Segway into more extravagant animations???
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
            EnterState(enemy);
        }

        else if (!_rc.InView && !_attacking || !inRange && !_attacking)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
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
