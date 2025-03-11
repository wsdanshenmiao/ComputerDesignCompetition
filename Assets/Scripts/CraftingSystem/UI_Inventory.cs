using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    // 合成台
    private Transform craftingTable;
    private Transform craftingSlotTemplate;
    
    // 物品栏
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    
    public float itemCellSize;
    [Range(3, 10)] public int maxX = 4;

    public float tableCellSize;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        
        craftingTable = transform.Find("CraftingTable");
        craftingSlotTemplate = craftingTable.Find("SlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        this.inventory.OnItemListChanged += OnItemListChanged;
        RefreshInventoryItems();
        RefreshCraftingTable();
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
            DragDrop dragDrop = trans.GetComponent<DragDrop>();
            dragDrop.SetTargetPos(item.GetTargetPos());
            Image image = trans.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x >= maxX) {
                x = 0;
                y++;
            }
        }
    }

    private void RefreshCraftingTable()
    {
        int x  = 0, y = 0;

        // 先清除
        foreach (Transform child in craftingTable) {
            if (child != craftingSlotTemplate) {
                Destroy(child.gameObject);
            }
        }

        // 生成新的UI
        Vector2 tableSize = CraftingSystem.Instance.craftingTableSize;
        for (int i = 0; i < tableSize.x * tableSize.y; ++i) {
            RectTransform trans = Instantiate(craftingSlotTemplate, craftingTable).GetComponent<RectTransform>();
            trans.gameObject.SetActive(true);
            trans.anchoredPosition = new Vector2(x * tableCellSize, -y * tableCellSize);
            x++;
            if (x >= tableSize.x) {
                x = 0;
                y++;
            }
        }
    }
}