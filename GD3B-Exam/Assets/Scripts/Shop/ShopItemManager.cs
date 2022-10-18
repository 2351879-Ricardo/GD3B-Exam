using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    
    private ShopItemSO _shopItemSo;
    
    public void InstantiateShopItem()
    {
        itemName.text = _shopItemSo.ItemName;
        itemImage.sprite = _shopItemSo.ItemSprite;
        GetComponentInChildren<ShopItemCostsManager>().UpdateCostsUI(_shopItemSo);
    }

    public ShopItemSO ShopItem
    {
        get => _shopItemSo;
        set => _shopItemSo = value;
    }

    private void UpdateItemUI()
    {
        
    }
}
