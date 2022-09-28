using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private CraftingSO _currentRecipe;
    public void CreateCraftEvent(CraftingSO recipe)
    {
        _currentRecipe = recipe;
        UpdateWeaponInfo();
        UpdateResourceRequirements();
    }

    private void UpdateWeaponInfo()
    {
        // update upper part of craft menu
    }

    private void UpdateResourceRequirements()
    {
        // Update lower part of craft menu
    }
    
    
}
