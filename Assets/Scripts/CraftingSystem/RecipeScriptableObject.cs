using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "CraftingSystem/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject outputItem;
    
    public ItemScriptableObject[] inputItem;
}