using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState CurrentState;
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyDodgeState DodgeState = new EnemyDodgeState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();

    private EnemyController _enemyController;
    private CharacterController _characterController;
    
    private void Start()
    {
        _enemyController = gameObject.GetComponent<EnemyController>();
        CurrentState = ChaseState;
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    public EnemyController EnemyController => _enemyController;
    public CharacterController Player => _characterController;
}
