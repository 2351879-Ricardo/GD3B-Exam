using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private float damageTick;
    [SerializeField] private float damage;
    [SerializeField] private float damageRadius;

    public float _currentHealth;
    public float _time;

    private GameObject _cnvs;
    private CharacterState _cs;
    private float _mit;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _cnvs = GameObject.FindGameObjectWithTag("Canvas");
        _cs = GetComponent<CharacterState>();
        _mit = GetComponentInChildren<Shield>().mitigation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            EndGame();
        }
    }

    public void TakeDamage(float dmg)
    {
        if (_cs.currentState == CharacterState.PlayerStates.IsBlocking)
        {
            dmg -= _mit*dmg;
        }
        _currentHealth -= dmg;
        _time = damageTick;
    }

    private void EndGame()
    {
        Cursor.visible = true;
        _cnvs.GetComponent<UIManager>().GameOver();
        GetComponent<PlayerInput>().enabled = false;
        
        // Test Spawn Options
        var spawnEnemies = FindObjectsOfType<SpawnEnemies>();
        foreach (var spawner in spawnEnemies)
        {
            spawner.StopSpawn();
        }
        
        Time.timeScale = 0f;
    }
}
