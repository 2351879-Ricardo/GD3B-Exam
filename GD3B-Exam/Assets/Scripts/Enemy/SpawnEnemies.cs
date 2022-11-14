using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnDelay, spawnDelay);
    }

    private void SpawnEnemy()
    {
        var randomValue = Random.Range(0, enemies.Count);
        Instantiate(enemies[randomValue], transform.position, Quaternion.identity);
    }

    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnEnemy));
    }

    public void LevelUp()
    {
        //level up difficulty of enemies
    }
}
