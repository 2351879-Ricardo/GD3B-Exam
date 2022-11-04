using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Debugger : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) _playerStats.TakeDamage(Random.Range(10, 50));
        if (Input.GetKeyDown(KeyCode.G)) _playerStats.AddHealthToPlayer(Random.Range(10, 50));
    }
}
