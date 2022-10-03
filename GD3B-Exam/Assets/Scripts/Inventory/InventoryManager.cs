using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ResourceSO resourceSo;
    public int resourceCount;
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private List<InventoryItem> inventory;
    
    private bool _inventoryOpen = false;

    public void AddToInventory(ResourceSO resource, int value)
    {
        foreach (var item in inventory)
        {
            if (item.resourceSo == resource)
            {
                item.resourceCount += value;
            }
        }
    }

    public void RemoveFromInventory(ResourceSO resource, int value)
    {
        foreach (var item in inventory)
        {
            if (item.resourceSo != resource) continue;
            if (item.resourceCount >= value) item.resourceCount -= value;
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
