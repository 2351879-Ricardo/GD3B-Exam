using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Heal,
    Speed,
    Damage
}

[CreateAssetMenu(menuName = "SO's/Shop Item", fileName = "NewShopItem")]
public class ShopItemSO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private List<InventoryItem> itemCosts;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private float upgradeValue;
    [SerializeField] private Sprite itemSprite;

    public string ItemName => itemName;
    public List<InventoryItem> ItemCosts => itemCosts;
    public UpgradeType ItemUpgradeType => upgradeType;
    public float UpgradeValue => upgradeValue;
    public Sprite ItemSprite => itemSprite;
}
