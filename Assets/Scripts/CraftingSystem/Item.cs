
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType : int
    {
        Magnet = 0, MetallicCard = 1, MagneticNeedle = 2, Bowl = 3, 
        Water = 4, WaterBowl = 5, Foam = 6, FoamNeedle = 7, 
        Compass = 8, ItemTypeCount = 9
    }
    
    public ItemScriptableObject itemScriptableObject;
    public uint amount;

    public Sprite GetSprite()
    {
        return itemScriptableObject.itemSprite;
    }

    public override string ToString()
    {
        return itemScriptableObject.name;
    }

    static public Sprite GetSprite(ItemType itemType)
    {
        Sprite sprite = CraftingSystem.Instance.GetItemSprite(itemType);
        if (sprite != null) {
            return sprite;
        }
        else {
            return null;
        }
    }
}