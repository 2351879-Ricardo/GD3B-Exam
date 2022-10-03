using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private GameObject itemImageGameObject;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    private ResourceSO _resourceSo;

    public void UpdateInternalUI(InventoryManager inventoryManager)
    {
        var inventory = inventoryManager.InventoryList;
        foreach (var item in inventory)
        {
            if (item.resourceSo == _resourceSo)
            {
                itemCountText.text = item.resourceCount.ToString();
            }
        }
    }

    public TextMeshProUGUI ItemNameText => itemNameText;
    public TextMeshProUGUI ItemCountText => itemCountText;

    public void SetResource(ResourceSO resource)
    {
        _resourceSo = resource;
        itemImageGameObject.GetComponent<Image>().sprite = _resourceSo.ItemSprite;
        itemNameText.text = _resourceSo.ItemName;
        itemCountText.text = "0";
    }
}
