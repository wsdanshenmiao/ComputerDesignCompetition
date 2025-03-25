
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType : int
    {
        StressMirrorBody, BronzeIngot, BronzeMirrorBlank, PotteryMould, 
        IceWater, Millstone, Calomelene, ChineseMagicMirror, 
        Wood, Chisel, IronNeedle, Magnet, JuncusRoemerianus, MagnetNeedle, 
        FloatingMagnetNeedle, Bassie, ClearWater, Compass, 
        StoneMortar, Saltpeter, SaltpeterPowder, CharcoalDust, BambooShovel,
        SulfurPowder, MixedPowder, BambooSieve, Gunpowder, 
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