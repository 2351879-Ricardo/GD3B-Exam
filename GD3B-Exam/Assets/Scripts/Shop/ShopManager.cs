using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    [SerializeField] private List<ShopItemSO> availableItems;
    [SerializeField] private int numberOfItems;

    [Header("UI")]
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    private void CreateShop()
    {
        ClearShop();
        for (var i = 0; i < numberOfItems; i++)
        {
            CreateShopItem();
        }
    }

    private void CreateShopItem()
    {
        var randomChosen = Random.Range(0, availableItems.Count);
        var item = Instantiate(shopItemPrefab, shopContainer.transform);
        var shopItemManager = item.GetComponent<ShopItemManager>();
        shopItemManager.ShopItem = availableItems[randomChosen];
        shopItemManager.InstantiateShopItem();
    }

    private void ClearShop()
    {
        foreach (Transform child in shopContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
