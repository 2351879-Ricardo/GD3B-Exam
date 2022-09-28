using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO's/Crafting", fileName = "NewCrafting")]
public class CraftingSO : ScriptableObject
{
    [Header("General Information")]
    [SerializeField] private string itemName;
    [TextArea(5, 15)] [SerializeField] private string itemDescription;

    [Header("Crafting Information")]
    [SerializeField] private WeaponSO craftedWeapon;
    [SerializeField] private List<InventoryItem> craftingIngredients;
    
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    
    public List<InventoryItem> CraftingIngredients => craftingIngredients;
    public WeaponSO CraftedWeapon => craftedWeapon;
}