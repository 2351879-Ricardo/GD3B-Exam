using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryResource : MonoBehaviour
{
    [SerializeField] private Image resourceImageUI;
    [SerializeField] private TextMeshProUGUI resourceAmountUI;
    
    private InventoryItem _inventoryItem;

    public void CreateResourceItem(InventoryItem invItem)
    {
        _inventoryItem = invItem;
        UpdateResourceItem();
    }

    private void UpdateResourceItem()
    {
        resourceImageUI.sprite = _inventoryItem.resourceSo.ItemSprite;
        resourceAmountUI.text = _inventoryItem.resourceCount.ToString();
    }
}
