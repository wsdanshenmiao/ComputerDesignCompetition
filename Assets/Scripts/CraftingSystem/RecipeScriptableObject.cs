using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Recipe", menuName = "CraftingSystem/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject outputItem;
    public uint amount = 1;
    
    public ItemScriptableObject[] inputItems = new ItemScriptableObject[(int)
        (CraftingSystem.craftingTableSize.x * CraftingSystem.craftingTableSize.y)];
}