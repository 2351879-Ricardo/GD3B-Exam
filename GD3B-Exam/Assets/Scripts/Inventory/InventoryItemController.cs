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
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].resourceSo == _resourceSo)
            {
                itemCountText.text = inventory[i].resourceCount.ToString();
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
