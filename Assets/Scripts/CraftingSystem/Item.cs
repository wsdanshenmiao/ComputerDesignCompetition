
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Magnet, MetallicCard, MagneticNeedle, Bowl, Water, WaterBowl, Foam, FoamNeedle, Compass
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
        Sprite sprite = null;
        if (ItemAssets.Instance.itemSpriteDictionary.TryGetValue(itemType, out sprite)) {
            return sprite;
        }
        else {
            Debug.LogError("You should add " + itemType.GetType().Name);
            return null;
        }
    }
}