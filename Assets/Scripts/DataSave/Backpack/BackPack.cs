using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BackPack : Singleton<BackPack>, ISaveable
{
    [SerializeField] private List<ItemData> dataList;

    public SavePriority LoadPriority => SavePriority.BackPack;

    public List<BackPackItem> FoodList;
    public Dictionary<ItemData, BackPackItem> FoodDictionary;

    public List<BackPackItem> IngredientItemsList;
    public Dictionary<ItemData_Ingredient, BackPackItem> IngredientDictionary;

    [Header("存档数据")]
    private List<BackPackItem> loadedItems = new List<BackPackItem>();
    private HashSet<IngredientType> unlockedIngredients = new HashSet<IngredientType>();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // 确保所有集合都被初始化
        FoodList = new List<BackPackItem>();
        FoodDictionary = new Dictionary<ItemData, BackPackItem>();
        IngredientItemsList = new List<BackPackItem>();
        IngredientDictionary = new Dictionary<ItemData_Ingredient, BackPackItem>();

        AddStartingItems();
    }

    private List<ItemData> GetItemDataBase()//获取物品数据库
    {
        //List<ItemData> itemDatabase = new List<ItemData>();
        //string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Item" });
        //foreach (string SOName in assetNames)
        //{
        //    var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
        //    var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
        //    itemDatabase.Add(itemData);
        //}
        List<ItemData> ret = new List<ItemData>();

        foreach (var item in dataList)
        {
            ret.Add(item);
        }

        return ret;
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && item.itemId == pair.Key)
                {
                    BackPackItem itemToLoad = new BackPackItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }

    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();

        foreach (KeyValuePair<ItemData, BackPackItem> pair in FoodDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData_Ingredient, BackPackItem> pair in IngredientDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }
    }

    public void AddIngredient(ItemData_Ingredient ingredient, int amount)
    {
        if (ingredient == null) return;

        // 检查是否是新获得的食材类型
        if (!unlockedIngredients.Contains(ingredient.ingredientType))
        {
            unlockedIngredients.Add(ingredient.ingredientType);
        }

        // 添加到食材字典
        if (IngredientDictionary.TryGetValue(ingredient, out BackPackItem value))
        {
            value.AddStack(amount);
        }
        else
        {
            BackPackItem newItem = new BackPackItem(ingredient);
            newItem.stackSize = amount;

            IngredientItemsList.Add(newItem);
            IngredientDictionary.Add(ingredient, newItem);
        }
    }

    public bool HasIngredientType(IngredientType type)
    {
        foreach (var pair in IngredientDictionary)
        {
            if (pair.Key.ingredientType == type)
                return true;
        }
        return false;
    }

    public void AddFood(ItemData_Food food, int amount = 1)
    {
        if (food == null) return;

        // 添加到食物字典
        if (FoodDictionary.TryGetValue(food, out BackPackItem value))
        {
            value.AddStack(amount);
        }
        else
        {
            BackPackItem newItem = new BackPackItem(food);
            newItem.stackSize = amount;

            FoodList.Add(newItem);
            FoodDictionary.Add(food, newItem);
        }
    }

    private void AddStartingItems()
    {
        if (loadedItems.Count > 0)
        {
            foreach (BackPackItem item in loadedItems)
            {
                AddItem(item.itemData, item.stackSize);
            }

            return;
        }
    }

    public void AddItem(ItemData _item, int _amountToAdd)
    {
        if (_item.type == ItemType.Material)
        {
            AddIngredient(_item as ItemData_Ingredient, _amountToAdd);
        }
        if (_item.type == ItemType.Food)
        {
            AddFood(_item as ItemData_Food, _amountToAdd);
        }
    }
}
