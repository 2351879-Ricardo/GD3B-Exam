using UnityEngine;

public class Interact : MonoBehaviour
{
    private LineOfSight _lineOfSight;
    private InventoryManager _inventoryManager;
    private InventoryUI _inventoryUI;

    private void Start()
    {
        _lineOfSight = FindObjectOfType<LineOfSight>();
        _inventoryManager = FindObjectOfType<InventoryManager>();
        _inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void OnInteraction()
    {
        if (_lineOfSight.hit.collider == null) return;
        if (_lineOfSight.hit.transform.CompareTag("Pickup"))
        {
            var resourceManager = _lineOfSight.hit.transform.gameObject.GetComponent<ResourceManager>();
            _inventoryManager.AddToInventory(resourceManager.resourceSo, resourceManager.resourceAmount);
            _inventoryUI.UpdateInventoryUI();
            Destroy(_lineOfSight.hit.transform.gameObject);
        }

        if (_lineOfSight.hit.transform.CompareTag("Crafting"))
        {
            // Disable Player Movement and Look Controls.            
        }
    }
}
