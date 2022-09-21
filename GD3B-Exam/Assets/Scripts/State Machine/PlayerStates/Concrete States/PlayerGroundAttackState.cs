using UnityEngine;

public class PlayerGroundAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter Ground Attack State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Update Ground Attack State");
    }
}