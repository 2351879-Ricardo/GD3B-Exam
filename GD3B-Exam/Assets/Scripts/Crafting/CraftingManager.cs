using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    [Header("Crafted Weapon Info")]
    [SerializeField] private Image craftedWeaponImageUI;
    [SerializeField] private TextMeshProUGUI craftedWeaponNameText;
    // Add Weapon Crafted Details
    
    [Header("Resource Requirements Info")]
    [SerializeField] private GameObject resourceBody;
    [SerializeField] private GameObject resourcePrefabUI;

    [Header("Crafting")] 
    [SerializeField] private Button craftButton;

    private bool _buttonStateCheck;
    
    
    private CraftingSO _currentRecipe;
    public void CreateCraftEvent(CraftingSO recipe)
    {
        _currentRecipe = recipe;
        UpdateWeaponInfo();
        UpdateResourceRequirements();
    }

    private void UpdateWeaponInfo()
    {
        craftedWeaponImageUI.sprite = _currentRecipe.CraftedWeapon.ItemSprite;
        craftedWeaponNameText.text = _currentRecipe.CraftedWeapon.ItemName;
        // Set Weapon Description Attributes Thing
    }

    private void UpdateResourceRequirements()
    {
        RemoveOldChildren();
        _buttonStateCheck = true;
        foreach (var resource in _currentRecipe.CraftingIngredients)
        {
            var resourceUI = Instantiate(resourcePrefabUI, resourceBody.transform);
            resourceUI.GetComponent<ResourceRequirementManager>().UpdateResourceUINode(resource, out var resourceValueCheck);
            _buttonStateCheck = _buttonStateCheck && resourceValueCheck;
        }
        SetCraftButtonState();
    }

    private void RemoveOldChildren()
    {
        foreach (Transform child in resourceBody.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClickCraftButton()
    {
        // Remove Resources
        // Create Weapon
        // Add to Inventory
        UpdateResourceRequirements();
    }

    private void SetCraftButtonState()
    {
        craftButton.interactable = _buttonStateCheck;
    }
}
