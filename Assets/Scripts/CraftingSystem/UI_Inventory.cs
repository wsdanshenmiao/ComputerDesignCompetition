using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public Vector2 craftingTableSize = new Vector2(3, 3);
    
    private Inventory inventory;

    // 合成台
    private Transform craftingTable;
    private Transform craftingSlotTemplate;
    
    // 物品栏
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    
    private List<DragDrop> itemSlots;
    private RectTransform[] craftingSlots;
    private RectTransform outputCraftingSlot;
    
    public float itemCellSize;
    [Range(3, 10)] public int maxX = 4;

    public float tableCellSize;

    private void Awake()
    {
        itemSlots = new List<DragDrop>();
        int size = (int)(craftingTableSize.x * craftingTableSize.y);
        craftingSlots = new RectTransform[size];
        
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        
        craftingTable = transform.Find("CraftingTable");
        craftingSlotTemplate = craftingTable.Find("SlotTemplate");
    }

    private void Update()
    {
        SetNearestCraftingSlot();
    }

    // 获取所有的合成槽
    public Transform[] GetCraftingTable()
    {
        return craftingSlots;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        this.inventory.OnItemListChanged += OnItemListChanged;
        RefreshInventoryItems();
        RefreshCraftingTable();
    }

    private void SetNearestCraftingSlot()
    {
        if (DragDrop.catchDrag == null) return;
        
        // 为物品设置最近的槽
        float minDis = float.MaxValue;
        Vector3 nearestPos = craftingSlots[0].position;
        Vector3 dragPos = DragDrop.catchDrag.GetComponent<RectTransform>().position;
        foreach (var slot in craftingSlots) {
            float dis = Vector3.Distance(slot.position, dragPos);
            if (dis < minDis) {
                minDis = dis;
                nearestPos = slot.position;
            }
        }
        DragDrop.catchDrag.SetTargetPos(nearestPos);
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
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + x * itemCellSize, 
                trans.anchoredPosition.y - y * itemCellSize);
            DragDrop dragDrop = trans.GetComponent<DragDrop>();
            dragDrop.SetTargetPos(item.GetTargetPos());
            Image image = trans.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x >= maxX) {
                x = 0;
                y++;
            }
            itemSlots.Add(dragDrop);
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
        Vector2 tableSize = craftingTableSize;
        for (int i = 0; i < tableSize.x * tableSize.y; ++i) {
            RectTransform trans = Instantiate(craftingSlotTemplate, craftingTable).GetComponent<RectTransform>();
            trans.gameObject.SetActive(true);
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + x * tableCellSize, 
                trans.anchoredPosition.y - y * tableCellSize);
            x++;
            if (x >= tableSize.x) {
                x = 0;
                y++;
            }
            craftingSlots[i] = trans;
        }
        
        // 最后一个为输出槽
        RectTransform outputTrans = Instantiate(craftingSlotTemplate, craftingTable).GetComponent<RectTransform>();
        outputTrans.gameObject.SetActive(true);
        float outputX = outputTrans.anchoredPosition.x + (int)tableSize.x * tableCellSize;
        float outputY = outputTrans.anchoredPosition.y - (int)(tableSize.y * 0.5f) * tableCellSize;
        outputTrans.anchoredPosition = new Vector2(outputX, outputY);
        
        outputCraftingSlot = outputTrans;
    }
}