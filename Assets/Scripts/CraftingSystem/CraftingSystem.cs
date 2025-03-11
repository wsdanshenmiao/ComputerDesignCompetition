using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : Singleton<CraftingSystem>
{
    public Vector2 craftingTableSize = new Vector2(9, 9);
    
    
    [SerializeField] private List<RecipeScriptableObject> recipes;
    
    
    
}