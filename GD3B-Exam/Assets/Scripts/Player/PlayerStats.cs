using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private float damageTick;
    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;

    public float _currentHealth;
    public float _time;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
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

    private void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        _time = damageTick;
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
