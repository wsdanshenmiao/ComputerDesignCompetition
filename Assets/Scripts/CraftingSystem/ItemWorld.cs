using System;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private Item item;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = item.GetSprite();
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player") && item.amount > 0) {
            CraftingSystem.Instance.AddItem(new Item(){ itemScriptableObject = item.itemScriptableObject, amount = item.amount });
            DestroySelf();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player") && item.amount > 0) {
            CraftingSystem.Instance.AddItem(item);
            DestroySelf();
        }
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