using UnityEngine;

public class RecipePrefabManager : MonoBehaviour
{
    private CraftingSO _craftingSo;

    public void CreateRecipeUI(CraftingSO craftingRecipe)
    {
        _craftingSo = craftingRecipe;
        // Updating Info on Clickable Button for Recipe (Recipe Menu)
    }

    public void SelectRecipe()
    {
        FindObjectOfType<CraftingManager>().CreateCraftEvent(_craftingSo);
    }
}
