
using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Magnet, MetallicCard, MagneticNeedle
    }
    
    public ItemScriptableObject itemScriptableObject;
    public int amount;

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

    public Vector3 GetTargetPos()
    {
        switch (itemScriptableObject.itemType) {
            default: 
            case ItemType.Magnet: return ItemAssets.Instance.magnetTargetPos.position;
            case ItemType.MetallicCard: return ItemAssets.Instance.metallicCardTargetPos.position;
            case ItemType.MagneticNeedle: return ItemAssets.Instance.magneticNeedleTargetPos.position;
        }
    }
}