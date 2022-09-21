using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Dodge State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Update Dodge State");
    }
}