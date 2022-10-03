using UnityEngine;

public class Interact : MonoBehaviour
{
    private LineOfSight _lineOfSight;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _lineOfSight = FindObjectOfType<LineOfSight>();
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void OnInteraction()
    {
        if (_lineOfSight.hit.transform.CompareTag("Pickup"))
        {
            
        }

        if (_lineOfSight.hit.transform.CompareTag("Crafting"))
        {
            // Disable Player Movement and Look Controls.
            
        }
    }
}
