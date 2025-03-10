using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Food")]
public class ItemData_Food : ItemData
{
    public FoodType FoodType;
    
    public float EffectValue; // 效果数值

    public void UseEffect()
    {
        var effect = FoodEffectFactory.CreateEffect(FoodType);
        if (effect != null)
        {
            effect.ApplyEffect();
        }
    }
}
    