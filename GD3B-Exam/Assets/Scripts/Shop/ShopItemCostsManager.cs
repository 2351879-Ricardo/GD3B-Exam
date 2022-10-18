using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCostsManager : MonoBehaviour
{
    [SerializeField] private GameObject costsContainer;
    [SerializeField] private GameObject costsPrefab;

    public void UpdateCostsUI(ShopItemSO shopItemSo)
    {
        foreach (var cost in shopItemSo.ItemCosts)
        {
            var temp = Instantiate(costsPrefab, costsContainer.transform);
            temp.GetComponent<CostHandler>().HandleCosts(cost);
        }
    }
}
