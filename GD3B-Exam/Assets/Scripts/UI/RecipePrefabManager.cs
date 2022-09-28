using UnityEngine;

public class RecipePrefabManager : MonoBehaviour
{
    private CraftingSO _craftingSo;

    public void CreateRecipeUI(CraftingSO craftingRecipe)
    {
        _craftingSo = craftingRecipe;
        // Creates the Prefab and Populates the Recipe List
    }

    public void SelectRecipe()
    {
        GameObject.FindObjectOfType<CraftingManager>().CreateCraftEvent(_craftingSo);
    }
}
