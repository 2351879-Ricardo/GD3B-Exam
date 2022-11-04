using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyChaseState : EnemyBaseState
{
    private Vector3 _enemyToPlayerVector3;

    private float _decisionTime;

    private RangeCheck _rc;    
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.enemyAnimator.SetBool("isWalking", true);
        enemy.enemyAnimator.SetFloat("walkSpeed", enemy.EnemyController.EnemySo.EnemySpeed/2f);
        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = (enemy.EnemyController.EnemySo.EnemySpeed);

        _rc = enemy.EnemyRangeCheck;
        _decisionTime = 0;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _decisionTime += Time.deltaTime;
        
        var inChargeRange = _rc.charge;
        var inMeleeRange = _rc.melee;

        var playerPos = enemy.PlayerGameObject.transform.position;
        enemy.EnemyNavMeshAgent.SetDestination(playerPos);

        if (_decisionTime >= _rc.CheckTime)
        {
            var randNum = Random.Range(0f, 1f);
            randNum = Mathf.Round(randNum * 10);
            randNum /= 10;
            
            Debug.Log(randNum);
            if (inChargeRange && !inMeleeRange && randNum <= enemy.EnemyController.EnemySo.ChargeChance)
            {
                enemy.SwitchState(enemy.ChargeState);
            }
            else if (!inChargeRange && randNum <= enemy.EnemyController.EnemySo.RangeChance)
            {
                enemy.SwitchState(enemy.RangeState);
            }
            
            _decisionTime = 0;
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