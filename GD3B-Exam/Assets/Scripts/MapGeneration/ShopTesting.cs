using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTesting : MonoBehaviour
{
    public Transform Enemy;
    Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(-100f, 0.5f, 0f);
        Invoke("SpawnTestEnemy", 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnTestEnemy()
    {
        Instantiate(Enemy, Position, Quaternion.identity);
        Position = new Vector3(Position.x + 10, 0.5f, 0f);
        Invoke("SpawnTestEnemy", 5f);
    }
}
