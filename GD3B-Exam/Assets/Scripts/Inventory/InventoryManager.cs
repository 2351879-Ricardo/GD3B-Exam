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

    private void Start()
    {
        if (FindObjectOfType<BossInit>() == null) return;
        inventory[0].resourceCount = SceneVariableTransfer.Inv1Amount;
        inventory[1].resourceCount = SceneVariableTransfer.Inv2Amount;
    }

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

    private void ToggleInventory()
    {
        _inventoryOpen = !_inventoryOpen;
        inventoryCanvas.SetActive(_inventoryOpen);

        if (!_inventoryOpen) return;
        var inventoryUI = FindObjectOfType<InventoryUI>();
        inventoryUI.UpdateInventoryUI();
    }

    public void OnOpenInventory()
    {
        ToggleInventory();
    }
    
    public List<InventoryItem> InventoryList => inventory;
}
