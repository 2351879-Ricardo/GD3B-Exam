using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Jump State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Update Jump State");
    }
}