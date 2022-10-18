using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostHandler : MonoBehaviour
{
    [SerializeField] private Image resourceImageUI;
    [SerializeField] private TextMeshProUGUI resourceValueUI;

    public void HandleCosts(InventoryItem cost)
    {
        resourceImageUI.sprite = cost.resourceSo.ItemSprite;
        resourceValueUI.text = cost.resourceCount.ToString();
    }
}
