using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private GameObject craftingUIPrefab;
    [SerializeField] private List<CraftingSO> craftingList;
    

    private void Start()
    {
        PopulateRecipes();
    }

    private void PopulateRecipes()
    {
        foreach (var recipe in craftingList)
        {
            var uiPrefab = Instantiate(craftingUIPrefab, transform);
            uiPrefab.GetComponent<RecipePrefabManager>().CreateRecipeUI(recipe);
        }
    }
}
