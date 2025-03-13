using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CraftingSystem : Singleton<CraftingSystem>
{
    #region Public
    // 合成台的大小
    static public int2 craftingTableSize = new int2(3, 3);
    #endregion


    #region Private

    // 背包的UI
    [SerializeField] private UI_Inventory uiInventory;
    // 现有库存
    private Inventory inventory;

    // 所有的物品
    [SerializeField] private List<ItemScriptableObject> items;
    // 所有的配方
    [SerializeField] private List<RecipeScriptableObject> recipes;

    // 所有的合成槽
    private CraftingSlot[] craftingSlots;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        inventory = new Inventory();
    }

    private void Start()
    {
        uiInventory.SetInventory(inventory);
        craftingSlots = uiInventory.GetCraftingTable();
        // Test
        inventory.AddItem(new Item()
        {
            itemScriptableObject = items.Find(
                delegate (ItemScriptableObject item) { return item.itemType == Item.ItemType.Magnet; }),
            amount = 4
        });
        inventory.AddItem(new Item()
        {
            itemScriptableObject = items.Find(
                delegate (ItemScriptableObject item) { return item.itemType == Item.ItemType.MetallicCard; }),
            amount = 4
        });
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
    }

    /// <summary>
    /// 根据现在合成台中的物品判断能合成什么物品
    /// </summary>
    /// <returns></returns> 能合成的物品，没有则返回null 
    public RecipeScriptableObject GetRecipes()
    {
        RecipeScriptableObject ret = null;
        foreach (RecipeScriptableObject recipe in recipes)
        {
            if (CheckRecipe(recipe))
            {
                ret = recipe;
                break;
            }
        }
        return ret;
    }

    private bool CheckRecipe(RecipeScriptableObject recipe)
    {
        for (int i = 0; i < craftingTableSize.x; i++)
        {
            for (int j = 0; j < craftingTableSize.y; j++)
            {
                // 都为空
                var item = recipe.inputItems[i + j * (int)craftingTableSize.x];
                var itemType = GetItemType(i, j);
                if (item == null || itemType == null)
                {
                    if (item != itemType)
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (item.itemType != itemType.itemType)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private ItemScriptableObject GetItemType(int x, int y)
    {
        if (x < 0 || y < 0 || x >= craftingTableSize.x || y >= craftingTableSize.y)
        {
            throw new System.IndexOutOfRangeException();
        }

        var item = craftingSlots[y * craftingTableSize.x + x].item;
        return item == null ? null : item.itemScriptableObject;
    }

}