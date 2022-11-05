using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChargeState : EnemyBaseState
{
    private float _chargeTime;
    private float _normalSpeed, _chargeSpeed;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Entered Charge State");

        _normalSpeed = enemy.EnemyController.EnemySo.EnemySpeed;
        _chargeSpeed = _normalSpeed * enemy.EnemyController.EnemySo.ChargeMult;

        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        enemy.enemyAnimator.SetBool("isCharging", true);
        _chargeTime = 0;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _chargeTime += Time.deltaTime;

        if (_chargeTime >= enemy.actionTime)
        {
            Debug.Log($"switching   {_chargeTime}");
            enemy.enemyAnimator.SetBool("isCharging", false);
            _chargeTime = 0;
            enemy.SwitchState(enemy.ChaseState);
        }
    }

    public override void EndAction(int i)
    {
        
    }
}
