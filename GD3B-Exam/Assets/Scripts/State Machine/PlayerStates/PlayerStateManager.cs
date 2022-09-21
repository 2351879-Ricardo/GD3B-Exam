using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private PlayerGroundAttackState _playerGroundAttackState = new PlayerGroundAttackState();
    private PlayerAirAttackState _playerAirAttackState = new PlayerAirAttackState();
    private PlayerJumpState _playerJumpState = new PlayerJumpState();
    private PlayerDodgeState _playerDodgeState = new PlayerDodgeState();
    private PlayerIdleState _playerIdleState = new PlayerIdleState();

    private void Start()
    {
        _currentState = _playerIdleState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    private void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
