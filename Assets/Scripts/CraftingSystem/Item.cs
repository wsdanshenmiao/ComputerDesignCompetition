
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Magnet, MetallicCard, Froth
    }
    
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType) {
            default:
            case ItemType.Magnet: return ItemAssets.Instance.magnetSprite;
            case ItemType.MetallicCard: return ItemAssets.Instance.metallicCardSprite;
        }
    }
}