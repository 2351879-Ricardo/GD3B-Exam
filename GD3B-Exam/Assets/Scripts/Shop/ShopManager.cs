using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    [SerializeField] private List<ShopItemSO> availableItems;
    [SerializeField] private int numberOfItems;

    [Header("UI")]
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private List<GameObject> navItems;
    private int _index = 0;

    private void Awake()
    {
        CreateShop();
    }

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
        navItems.Add(item);
    }

    private void ClearShop()
    {
        foreach (Transform child in shopContainer.transform)
        {
            Destroy(child.gameObject);
            navItems.RemoveAt(navItems.Count-1);
        }
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        gameObject.SetActive(false);
    }

    public void NavigateUp()
    {
        _index--;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    public void NavigateDown()
    {
        _index++;
        if (_index < 0) _index = navItems.Count-1;
        if (_index >= navItems.Count) _index = 0;
        EventSystem.current.SetSelectedGameObject(navItems[_index]);
    }

    public void OnUp()
    {
        Debug.Log("Goinnnn Up");
    }

    public void OnDOwn()
    {
        Debug.Log("Goinnnnn Downn");
    }
}
