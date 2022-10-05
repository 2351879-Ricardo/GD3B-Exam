using System;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
