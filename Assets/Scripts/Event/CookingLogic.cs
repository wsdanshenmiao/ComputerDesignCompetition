using System.Collections.Generic;
using UnityEngine;

public class CookingLogic : MonoBehaviour
{
    [SerializeField] private Recipe[] allRecipes;
    [SerializeField] private BackPack backpack;
    
    private int maxSlots = 2;              // 最大栏位数
    private int unlockedIngredientCount;   // 已解锁的食材数量
    private int availableSlots;            // 当前可用栏位
    
    public Dictionary<IngredientType, int> currentIngredients = new Dictionary<IngredientType, int>();
    
    private void Start()
    {
        if (backpack == null)
            backpack = FindObjectOfType<BackPack>();
            
        availableSlots = maxSlots;
    }
    
    // 解锁新的栏位
    public void UnlockNewSlots()
    {
        unlockedIngredientCount++;
        
        if (unlockedIngredientCount == 1)
            maxSlots += 3;    // 第一次解锁+3
        else if (unlockedIngredientCount == 2)
            maxSlots += 2;    // 第二次解锁+2
            
        maxSlots = Mathf.Min(maxSlots, 7);  // 确保不超过7个栏位
        availableSlots = maxSlots;          // 更新可用栏位
    }
    
    // 检查是否还有空余栏位
    public bool HasAvailableSlot()
    {
        return GetTotalIngredients() < availableSlots;
    }
    
    // 获取当前食材总数
    private int GetTotalIngredients()
    {
        int total = 0;
        foreach (var pair in currentIngredients)
        {
            total += pair.Value;
        }
        return total;
    }
        public bool AddIngredient(ItemData_Ingredient ingredient)
    {
        if (!HasAvailableSlot()) return false;
        
        // 检查背包中是否有这种食材
        if (!backpack.HasIngredientType(ingredient.ingredientType))
        {
            Debug.LogWarning($"背包中没有{ingredient.ingredientType}这种食材！");
            return false;
        }
        
        var type = ingredient.ingredientType;
        if (!currentIngredients.ContainsKey(type))
            currentIngredients[type] = 1;
        else
            currentIngredients[type]++;
            
        return true;
    }

    public void RemoveIngredient(IngredientType type)
    {
        if (currentIngredients.ContainsKey(type))
        {
            currentIngredients[type]--;
            if (currentIngredients[type] <= 0)
                currentIngredients.Remove(type);
        }
    }
    
    // 尝试烹饪
    public ItemData_Food TryCook()
    {
        if (currentIngredients.Count == 0)
            return null;
            
        foreach (var recipe in allRecipes)
        {
            if (recipe.MatchRecipe(currentIngredients))
            {
                ItemData_Food food = new ItemData_Food();
                food.FoodType = recipe.resultFood;
                
                 // 计算使用的栏位数
                int usedSlots = 0;
                foreach (var ingredient in recipe.ingredients)
                {
                    usedSlots += ingredient.amount;
                }
                
                // 更新可用栏位
                availableSlots = maxSlots - usedSlots;
                
                // 清空所有食材
                currentIngredients.Clear();
                
                // 使用效果
                food.UseEffect();

                // 将制作的食物添加到背包中
                backpack.AddFood(food);

                return food;
            }
        }
        
        Debug.LogWarning("没有找到匹配的食谱！");
        return null;
    }
    
    // 清空所有食材(没有煮，食材清空)
    public void ReStartCooking()
    {
        currentIngredients.Clear();
        availableSlots = maxSlots;

    }
    
    // 获取当前栏位数量
    public int GetMaxSlots() => maxSlots;
    
    // 获取已使用的栏位数量
    public int GetUsedSlots() => maxSlots - availableSlots;
    

} 