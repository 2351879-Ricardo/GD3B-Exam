using Unity.VisualScripting;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    public void MakePurchase()
    {
        if (!CanBuyItem()) return;
        DeductResources();
        UpdateInventoryInfoUI();
        UsePurchasedItem();
        RemoveItem();
    }

    private bool CanBuyItem()
    {
        var playerInventory = FindObjectOfType<InventoryManager>();
        var purchaseItem = GetComponentInChildren<ShopItemManager>();

        foreach (var craftingResource in purchaseItem.ShopItem.ItemCosts)
        {
            foreach (var resource in playerInventory.InventoryList)
            {
                if (resource.resourceSo != craftingResource.resourceSo) continue;
                if (resource.resourceCount < craftingResource.resourceCount) return false;
            }
        }

        return true;
    }

    private void DeductResources()
    {
        var playerInventory = FindObjectOfType<InventoryManager>();
        var purchaseItem = GetComponentInChildren<ShopItemManager>();

        foreach (var craftingResource in purchaseItem.ShopItem.ItemCosts)
        {
            playerInventory.RemoveFromInventory(craftingResource.resourceSo, craftingResource.resourceCount);
        }
    }

    private void UsePurchasedItem()
    {
        var purchaseItem = GetComponentInChildren<ShopItemManager>();
        var weapon = FindObjectOfType<PlayerStats>().GetComponentInChildren<Weapon>();

        switch (purchaseItem.ShopItem.ItemUpgradeType)
        {
            case UpgradeType.Damage:
                weapon.PlayerWeapon.attackDamage += purchaseItem.ShopItem.UpgradeValue;
                weapon.UpdateWeaponDamage();
                break;
            
            case UpgradeType.Speed:
                weapon.PlayerWeapon.attackSpeed += purchaseItem.ShopItem.UpgradeValue;
                weapon.UpdateWeaponSpeed();
                break;
            
            case UpgradeType.Heal:
                var playerStats = FindObjectOfType<PlayerStats>();
                playerStats.AddHealthToPlayer((int)purchaseItem.ShopItem.UpgradeValue);
                break;
        }
    }

    private void UpdateInventoryInfoUI()
    {
        var thisShop = GetComponentInParent<ShopManager>();
        thisShop.UpdateInventoryResourcesUI();
    }

    private void RemoveItem()
    {
        var shopManager = GetComponentInParent<ShopManager>();
        shopManager.RemoveUpgrade(gameObject);
    }
}
