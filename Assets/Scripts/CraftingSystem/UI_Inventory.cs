using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    
    public float itemCellSize;
    [Range(3, 10)] public int maxX = 4;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        this.inventory.OnItemListChanged += OnItemListChanged;
        RefreshInventoryItems();
    }

    private void OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x  = 0, y = 0;

        foreach (Transform child in itemSlotContainer) {
            if (child != itemSlotTemplate) {
                Destroy(child.gameObject);
            }
        }

        foreach (var item in inventory.GetItems()) {
            RectTransform trans = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            trans.gameObject.SetActive(true);
            trans.anchoredPosition = new Vector2(x * itemCellSize, -y * itemCellSize);
            Image image = trans.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x >= maxX) {
                x = 0;
                y++;
            }
        }
    }
}