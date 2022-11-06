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

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(_enemy.transform.position, _targetDir);
    }
    
    public override void EnterState(EnemyStateManager enemy)
    {
        _enemy = enemy;

        _normalSpeed = enemy.EnemyController.EnemySo.EnemySpeed;
        _chargeSpeed = _normalSpeed * enemy.EnemyController.EnemySo.ChargeMult;

        enemy.EnemyController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        enemy.enemyAnimator.SetBool("isCharging", true);
        _chargeTime = 0;
        _targetDir =  - _enemy.PlayerGameObject.transform.position - _enemy.transform.position;
        _targetDir = _targetDir.normalized;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        _chargeTime += Time.deltaTime;

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
        
        _enemy.gameObject.GetComponent<NavMeshAgent>().velocity = _targetDir * _chargeSpeed;
    }
}
