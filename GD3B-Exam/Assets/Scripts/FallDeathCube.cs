using System;
using UnityEngine;

public class FallDeathCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Stop Game");
        Debug.Log("Game Over");
    }
}
