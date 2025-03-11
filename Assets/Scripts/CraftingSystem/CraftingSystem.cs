using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : Singleton<CraftingSystem>
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;
    
    [SerializeField] private List<ItemScriptableObject> items;
    [SerializeField] private List<RecipeScriptableObject> recipes;

    protected override void Awake()
    {
        base.Awake();
        inventory = new Inventory();
    }

    private void Start()
    {
        uiInventory.SetInventory(inventory);
        
        // Test
        inventory.AddItem(new Item()
        {
            itemScriptableObject = items.Find(
                delegate(ItemScriptableObject item) { return item.itemType == Item.ItemType.Magnet; }),
            amount = 1
        });
        inventory.AddItem(new Item()
        {
            itemScriptableObject = items.Find(
                delegate(ItemScriptableObject item) { return item.itemType == Item.ItemType.MetallicCard; }),
            amount = 1
        });
    }
}