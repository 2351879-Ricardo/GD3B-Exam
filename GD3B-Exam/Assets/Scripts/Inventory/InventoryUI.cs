using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform inventoryBody;
    [SerializeField] private GameObject itemUIPrefab;
    
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
        PopulateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform childUI in inventoryBody)
        {
            InventoryItemController temp = childUI.GetComponent<InventoryItemController>();
            temp.UpdateInternalUI(_inventoryManager);
        }
    }

    private void PopulateInventoryUI()
    {
        foreach (var resource in _inventoryManager.InventoryList)
        {
            var newUIElement = Instantiate(itemUIPrefab, inventoryBody);
            newUIElement.GetComponent<InventoryItemController>().SetResource(resource.resourceSo);
            newUIElement.GetComponent<InventoryItemController>().UpdateInternalUI(_inventoryManager);
        }
    }
}
