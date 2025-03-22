using System;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform trans = Instantiate(CraftingSystem.Instance.itemPrefab, position, Quaternion.identity);
        
        ItemWorld itemWorld = trans.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        
        return itemWorld;
    }


    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}