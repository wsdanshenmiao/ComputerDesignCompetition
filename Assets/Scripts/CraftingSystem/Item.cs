
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
            case ItemType.Bowl: return ItemAssets.Instance.bowlSprite;
            case ItemType.Water: return ItemAssets.Instance.waterSprite;
            case ItemType.WaterBowl: return ItemAssets.Instance.waterBowlSprite;
            case ItemType.Foam: return ItemAssets.Instance.foamSprite;
            case ItemType.FoamNeedle: return ItemAssets.Instance.foamNeedleSprite;
            case ItemType.Compass: return ItemAssets.Instance.compassSprite;
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