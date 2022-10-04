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
        var layerMask = 1 << 8;
        var hit = Physics.CheckSphere(transform.position, damageRadius, layerMask);

        _time -= Time.deltaTime;

        if (hit && _time <0f)
        {
            TakeDamage(damage);
        }

        if (_currentHealth <= 0)
        {
            EndGame();
        }
    }

    private void TakeDamage(float dmg)
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
        _cnvs.GetComponent<UIManager>().GameOver();
        GetComponent<PlayerInput>().enabled = false;
    }
}
