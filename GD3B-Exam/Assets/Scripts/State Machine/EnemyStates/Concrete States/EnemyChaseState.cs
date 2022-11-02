using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private Vector3 _enemyToPlayerVector3;

    private float _decisionTime = 1f;

    private RangeCheck _rc;    
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.enemyAnimator.SetBool("isWalking", true);
        enemy.enemyAnimator.SetFloat("walkSpeed", enemy.EnemyController.EnemySo.EnemySpeed/2f);
        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = (enemy.EnemyController.EnemySo.EnemySpeed);

        _rc = enemy.EnemyRangeCheck;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        var inChargeRange = _rc.charge;
        var inMeleeRange = _rc.melee;

        var playerPos = enemy.PlayerGameObject.transform.position;
        enemy.EnemyNavMeshAgent.SetDestination(playerPos);

        if (inChargeRange && !inMeleeRange)
        {
            enemy.SwitchState(enemy.ChargeState);
        }

        if (!inChargeRange)
        {
            enemy.SwitchState(enemy.RangeState);
        }
        
        _enemyToPlayerVector3 = enemy.EnemyController.gameObject.transform.position - playerPos;
        if (_enemyToPlayerVector3.magnitude <= enemy.EnemyController.EnemySo.EnemyAttackRange && enemy.enemyAnimator)
        {
            enemy.SwitchState(enemy.AttackState);
            enemy.enemyAnimator.SetBool("isWalking", false);
        }

        if (_enemyToPlayerVector3.magnitude >= enemy.despawnDistance)
        {
            enemy.SwitchState(enemy.DespawnState);
        }
        
        
    }

    public override void EndAction(int i)
    {
        //Doing a thing
    }
}