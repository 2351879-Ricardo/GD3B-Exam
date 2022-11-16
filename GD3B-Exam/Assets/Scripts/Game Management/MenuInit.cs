using System;
using UnityEngine;

public class MenuInit : MonoBehaviour
{
    public MenuInputActions MenuInputs;
    
    private void Awake()
    {
        MenuInputs = new MenuInputActions();
    }
}
