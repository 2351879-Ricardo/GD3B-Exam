using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private LineOfSight lineOfSight;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private InventoryUI inventoryUI;

    public void OnInteraction()
    {
        if (lineOfSight.hit.collider == null) return;
        if (lineOfSight.hit.transform.CompareTag("Pickup"))
        {
            var resourceManager = lineOfSight.hit.transform.gameObject.GetComponent<ResourceManager>();
            inventoryManager.AddToInventory(resourceManager.resourceSo, resourceManager.ResourceAmount);
            inventoryUI.UpdateInventoryUI();
            Destroy(lineOfSight.hit.transform.gameObject);
        }

        if (lineOfSight.hit.transform.CompareTag("Crafting"))
        {
            // Disable Player Movement and Look Controls.            
        }
    }
}
