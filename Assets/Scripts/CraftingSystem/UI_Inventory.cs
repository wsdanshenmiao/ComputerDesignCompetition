using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x  = 0, y = 0;
        float itemInventoryCellSize = 30;

        foreach (var item in inventory.GetItems()) {
            RectTransform trans = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            trans.gameObject.SetActive(true);
            trans.anchoredPosition = new Vector2(x * itemInventoryCellSize, y * itemInventoryCellSize);
            Image image = trans.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 4) {
                x = 0;
                y++;
            }
        }
    }
}