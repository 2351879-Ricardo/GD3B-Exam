using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState _currentState;
    public float despawnDistance = 100f;
    public float actionTime = 1f;

    public Animator enemyAnimator;
    
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyDodgeState DodgeState = new EnemyDodgeState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyDespawnState DespawnState = new EnemyDespawnState();
    public EnemyChargeState ChargeState = new EnemyChargeState();
    public EnemyRangeState RangeState = new EnemyRangeState();

    private EnemyController _enemyController;
    private GameObject _playerGameObject;
    private PlayerStats _playerStats;
    [SerializeField]private RangeCheck rangeCheck;

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

    public void ExitAction(int i)
    {
        _currentState.EndAction(i);
    }

    public void DealDamage(float dmg)
    {
        Debug.Log("Hit Landed");
        Debug.Log(dmg);
        _playerStats.TakeDamage(dmg);
    }

    public void Flinch()
    {
        if (_enemyController.EnemySo.CanFlinch)
        {
            enemyAnimator.SetBool("Flinch", true);
            SwitchState(ChaseState);
        }
    }

    public void EndFlinch()
    {
        enemyAnimator.SetBool("Flinch", false);
    }

    public EnemyController EnemyController => _enemyController;
    public GameObject PlayerGameObject => _playerGameObject;
    public NavMeshAgent EnemyNavMeshAgent => _navMeshAgent;
    public PlayerStats PlayerStats => _playerStats;
    public RangeCheck EnemyRangeCheck => rangeCheck;
}
