
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType : int
    {
        ChineseMagicMirror, BronzeIngot, BronzeMirrorBlank, PotteryMould, 
        IceWater, Millstone, Calomelene, StressMirrorBody, 
        ItemTypeCount
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