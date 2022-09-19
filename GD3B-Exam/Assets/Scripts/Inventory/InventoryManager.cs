using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public ResourceSO resourceSo;
    public int resourceCount;

    public void AddResource(int value)
    {
        resourceCount += value;
    }

    public void RemoveResource(int value)
    {
        resourceCount -= value;
    }
}
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private List<InventoryItem> inventory;
    
    private bool _inventoryOpen = false;

    // // Test Functionality
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.I))
    //     {
    //         ToggleInventory();
    //     }
    // }

    public void AddToInventory(ResourceSO resource, int value)
    {
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].resourceSo == resource)
            {
                inventory[i].AddResource(value);
            }
        }
    }

    public void RemoveFromInventory(ResourceSO resource, int value)
    {
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].resourceSo != resource) continue;
            if (inventory[i].resourceCount >= value) inventory[i].RemoveResource(value);
            break;
        }
    }

    public void ToggleInventory()
    {
        _inventoryOpen = !_inventoryOpen;
        inventoryCanvas.SetActive(_inventoryOpen);
    }
    
    public List<InventoryItem> InventoryList => inventory;
}
