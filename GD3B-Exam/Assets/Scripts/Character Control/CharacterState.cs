using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public enum PlayerStates
    {
        IsBlocking,
        IsAttacking,
        IsMoving,
        IsCharging,
        IsDodging,
        IsIdle
    };

    public PlayerStates currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}