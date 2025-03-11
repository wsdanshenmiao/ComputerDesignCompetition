using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CraftingSystem/Item")]
public class ItemScriptableObject : ScriptableObject
{
        public string itemName;
        public Item.ItemType itemType;
        public Sprite itemSprite;
        
}