using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    public void MakePurchase()
    {
        if (CanBuyItem())
        {
            DeductResources();
            UsePurchasedItem();
        }
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
            foreach (var resource in playerInventory.InventoryList)
            {
                if (resource.resourceSo == craftingResource.resourceSo)
                {
                    resource.resourceCount -= craftingResource.resourceCount;
                }
            }
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
}
