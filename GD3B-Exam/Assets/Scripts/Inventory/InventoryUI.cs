using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform inventoryBody;
    [SerializeField] private GameObject itemUIPrefab;

    private void Start()
    {
        PopulateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        foreach (InventoryItemController childUI in inventoryBody)
        {
            childUI.UpdateInternalUI(inventoryManager);
        }
    }

    private void PopulateInventoryUI()
    {
        foreach (var resource in inventoryManager.InventoryList)
        {
            var newUIElement = Instantiate(itemUIPrefab, inventoryBody);
            newUIElement.GetComponent<InventoryItemController>().SetResource(resource.resourceSo);
            newUIElement.GetComponent<InventoryItemController>().UpdateInternalUI(inventoryManager);
        }
    }
}
