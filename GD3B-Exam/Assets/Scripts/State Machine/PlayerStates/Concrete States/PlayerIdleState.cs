using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Idle State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Update Idle State");
    }
}