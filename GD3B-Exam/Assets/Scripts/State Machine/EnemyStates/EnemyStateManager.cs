using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState _currentState;
    
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyDodgeState DodgeState = new EnemyDodgeState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();

    private EnemyController _enemyController;
    private GameObject _playerGameObject;
    private PlayerStats _playerStats;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _enemyController = gameObject.GetComponent<EnemyController>();
        _playerGameObject = FindObjectOfType<CharacterState>().gameObject;
        _playerStats = _playerGameObject.GetComponent<PlayerStats>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _currentState = ChaseState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public EnemyController EnemyController => _enemyController;
    public GameObject PlayerGameObject => _playerGameObject;
    public NavMeshAgent EnemyNavMeshAgent => _navMeshAgent;
    public PlayerStats PlayerStats => _playerStats;
}
