using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRequirementManager : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceNameText;
    [SerializeField] private TextMeshProUGUI valueText;

    private InventoryItem _resourceItem;
    
    public void UpdateResourceUINode(InventoryItem item, out bool hasEnough)
    {
        _resourceItem = item;
        InitItemDetails();
        UpdateResourceValues(out hasEnough);
    }

    private void InitItemDetails()
    {
        resourceIcon.sprite = _resourceItem.resourceSo.ItemSprite;
        resourceNameText.text = _resourceItem.resourceSo.ItemName;
    }

    private void UpdateResourceValues(out bool hasEnough)
    {
        hasEnough = false;
        var playerInventory = GameObject.FindObjectOfType<InventoryManager>();
        foreach (var resource in playerInventory.InventoryList)
        {
            if (resource.resourceSo == _resourceItem.resourceSo)
            {
                valueText.text = resource.resourceCount.ToString() + "/" + _resourceItem.resourceCount.ToString();
                hasEnough = (resource.resourceCount >= _resourceItem.resourceCount);
            }
        }
    }
}
