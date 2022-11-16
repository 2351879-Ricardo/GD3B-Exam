using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemySpawnClass
{
    public GameObject enemyPrefab;
    public float endTimer;
}

public class DynamicSpawner : MonoBehaviour
{
    [SerializeField] private float spawnChance01;
    [SerializeField] private List<EnemySpawnClass> enemySpawnTypes;
    [SerializeField] private GameObject spawnLocation;

    private float _timer = 0f;

    private void Start()
    {
        if (SpawnSucceeded())
        {
            SpawnEnemy();
        }
    }

    private bool SpawnSucceeded()
    {
        return (Random.Range(0f, 1f) <= spawnChance01);
    }

    private void SpawnEnemy()
    {
        UpdateTimer();
        var statePos = GetSpawnState(_timer);
        Instantiate(enemySpawnTypes[statePos].enemyPrefab, spawnLocation.transform.position, Quaternion.identity);
    }

    private void UpdateTimer()
    {
        _timer = FindObjectOfType<MapGenerator>().Timer;
    }

    private int GetSpawnState(float currentTime)
    {
        for (var i = 0; i < enemySpawnTypes.Count - 1; i++)
        {
            if (_timer <= enemySpawnTypes[i].endTimer)
            {
                Debug.Log("Test");
                return i;
            }
        }
        
        return enemySpawnTypes.Count - 1;
    }
}
