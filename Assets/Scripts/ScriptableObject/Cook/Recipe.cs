using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Data/Recipe")]
public class Recipe : ScriptableObject
{
    [Serializable]
    public struct IngredientRequirement
    {
        public IngredientType type;
        public int amount;
    }

    public FoodType resultFood;                    // 制作的食物类型
    public IngredientRequirement[] ingredients;    // 所需食材
    // 检查是否完全匹配配方
    public bool MatchRecipe(Dictionary<IngredientType, int> currentIngredients)
    {
        // 检查所需食材数量是否匹配
        foreach (var requirement in ingredients)
        {
            if (!currentIngredients.TryGetValue(requirement.type, out int amount) 
                || amount != requirement.amount)
                return false;
        }
        
        // 确保没有食谱中没有的食材
        foreach (var ingredient in currentIngredients)
        {
            bool isRequired = false;
            foreach (var requirement in ingredients)
            {
                if (requirement.type == ingredient.Key)
                {
                    isRequired = true;
                    break;
                }
            }
            if (!isRequired)
                return false;
        }
        
        return true;
    }
} 