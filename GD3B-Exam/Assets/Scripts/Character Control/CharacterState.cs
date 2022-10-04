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

    public bool _isBlocking;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blocking()
    {
        _isBlocking = !_isBlocking;
        if (_isBlocking)
            currentState = PlayerStates.IsBlocking;
        else
            currentState = PlayerStates.IsIdle;
    }

    public void StartAttack()
    {
        currentState = PlayerStates.IsAttacking;
    }

    public void EndAttack()
    {
        currentState = PlayerStates.IsIdle;
    }
}
