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

    private bool _isBlocking, _paused;
    
    // Start is called before the first frame update
    void Start()
    {
        _paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_paused)
        {
            Time.timeScale = 0f;
        }
        else if (!_paused)
        {
            Time.timeScale = 1f;
        }
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

    public void OnPauseGame()
    {
        Debug.Log("Pausing");
        _paused = !_paused;
        
    }
}
