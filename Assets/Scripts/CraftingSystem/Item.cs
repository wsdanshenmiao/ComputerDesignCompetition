
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Magnet, MetallicCard, MagneticNeedle, 
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
        switch (itemType) {
            default:
            case ItemType.Magnet: return ItemAssets.Instance.magnetSprite;
            case ItemType.MetallicCard: return ItemAssets.Instance.metallicCardSprite;
            case ItemType.MagneticNeedle: return ItemAssets.Instance.magneticNeedleSprite;
        }
    }
}