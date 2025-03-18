using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemAssets : Singleton<ItemAssets>
{
    [Serializable]
    struct ItemSprite
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }    
    
    public Transform itemPrefab;

    [SerializeField] private List<ItemSprite> itemSprites = new List<ItemSprite>();
    public Dictionary<Item.ItemType, Sprite> itemSpriteDictionary = new Dictionary<Item.ItemType, Sprite>();

    protected override void Awake() 
    {
        base.Awake();
        for (int i = 0; i < itemSprites.Count; i++) {
            itemSpriteDictionary.Add(itemSprites[i].itemType, itemSprites[i].sprite);
        }
    }
}