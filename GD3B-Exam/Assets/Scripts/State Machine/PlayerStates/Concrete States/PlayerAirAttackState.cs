using UnityEngine;

public class PlayerAirAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Air Attack State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Update Air Attack State");
    }
}