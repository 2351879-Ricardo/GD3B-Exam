using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChargeState : EnemyBaseState
{
    private float _chargeTime;
    private float _normalSpeed, _chargeSpeed;
    private EnemyStateManager _enemy;
    private Vector3 _targetDir;


    
    public override void EnterState(EnemyStateManager enemy)
    {
        _enemy = enemy;

        _normalSpeed = enemy.EnemyController.EnemySo.EnemySpeed;
        _chargeSpeed = _normalSpeed * enemy.EnemyController.EnemySo.ChargeMult;

        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().destination =
            enemy.PlayerGameObject.transform.position;
        enemy.enemyAnimator.SetBool("isCharging", true);
        _chargeTime = 0;
        
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _chargeTime += Time.deltaTime;

        var v = enemy.transform.forward;
        var pos = enemy.transform.position;

        enemy.gameObject.GetComponent<NavMeshAgent>().destination = pos + (v * 5);

        if (_chargeTime >= enemy.actionTime)
        {
            enemy.enemyAnimator.SetBool("isCharging", false);
            _chargeTime = 0;
            enemy.SwitchState(enemy.ChaseState);
        }
    }

    public override void EndAction(int i)
    {
        _enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = _chargeSpeed;
    }
}
