using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    #region Public
    public float itemCellSize;
    public float tableCellSize;

    public uint itemCellX = 4;
    #endregion

    #region Private

    private Inventory inventory;

    // 合成事件监听
    [SerializeField] private VoidEventSO CompoundEvent;

    // 合成台
    private Transform craftingTable;
    private Transform craftingSlotTemplate;

    // 物品栏
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    // 所有的物品
    private List<DragDrop> itemSlots;
    // 所有的合成槽
    private CraftingSlot[] craftingSlots;
    // 合成输出槽
    private CraftingSlot outputCraftingSlot;
    #endregion


    private void Awake()
    {
        itemSlots = new List<DragDrop>();
        int2 craftingTableSize = CraftingSystem.craftingTableSize;
        int size = craftingTableSize.x * craftingTableSize.y;
        craftingSlots = new CraftingSlot[size];
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        craftingTable = transform.Find("CraftingTable");
        craftingSlotTemplate = craftingTable.Find("SlotTemplate");
    }

    private void OnEnable()
    {
        CompoundEvent.OnEventRaised += OnCompound;
    }



    private void Update()
    {
        SetNearestCraftingSlot();
        SetOutputSlot();
        /*foreach (var slot in craftingSlots) {
            Debug.Log(slot.index);
            Debug.Log(slot.item);
        }*/
    }

    private void OnDisable()
    {
        CompoundEvent.OnEventRaised -= OnCompound;
    }


    // 获取所有的合成槽
    public CraftingSlot[] GetCraftingTable()
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


    private void OnCompound()
    {
        foreach (var slot in craftingSlots)
        {
            if (slot.item != null)
            {
                CraftingSystem.Instance.RemoveItem(slot.item);
            }
            slot.item = null;
        }
    }

    private void SetOutputSlot()
    {
        // 检测当前是否能合成物品
        var recipe = CraftingSystem.Instance.GetRecipes();
        if (recipe == null)
        {
            outputCraftingSlot.item = null;

        }
        else if (outputCraftingSlot.item == null)
        {
            outputCraftingSlot.item = new Item()
            {
                itemScriptableObject = recipe.outputItem,
                amount = recipe.amount
            };
        }
    }

    private void SetNearestCraftingSlot()
    {
        // 为物品设置最近的槽
        float minDis = float.MaxValue;
        CraftingSlot nearest = craftingSlots[0];
        Vector3 mousePosition = Input.mousePosition;
        foreach (var slot in craftingSlots)
        {
            float dis = Vector3.Distance(slot.rectTransform.position, mousePosition);
            if (dis < minDis)
            {
                minDis = dis;
                nearest = slot;
            }
        }

        DragDrop.target = nearest;
    }

    private CraftingSlot[] GetCraftingSlots()
    {
        return craftingSlots;
    }

    private void OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child != itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        int x = 0, y = 0;
        foreach (var item in inventory.GetItems())
        {
            for (int i = 0; i < item.amount; i++) {
                RectTransform trans = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                trans.gameObject.SetActive(true);
                trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + x * itemCellSize,
                    trans.anchoredPosition.y - y * itemCellSize);
                DragDrop dragDrop = trans.GetComponent<DragDrop>();
                dragDrop.SetItem(item);
                Image image = trans.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                x++;
                if (x >= itemCellX)
                {
                    x = 0;
                    y++;
                }
                itemSlots.Add(dragDrop);
            }
        }
    }

    private void RefreshCraftingTable()
    {
        int x = 0, y = 0;

        // 先清除
        foreach (Transform child in craftingTable)
        {
            if (child != craftingSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        // 生成新的UI
        int2 tableSize = CraftingSystem.craftingTableSize;
        for (int i = 0; i < tableSize.y; ++i)
        {
            for (int j = 0; j < tableSize.x; ++j)
            {
                CraftingSlot slot = Instantiate(craftingSlotTemplate, craftingTable).GetComponent<CraftingSlot>();
                slot.gameObject.SetActive(true);
                slot.index = new int2(j, i);
                slot.item = null;
                slot.isOutputSlot = false;
                RectTransform trans = slot.rectTransform;
                trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + x * tableCellSize,
                    trans.anchoredPosition.y - y * tableCellSize);
                x++;
                if (x >= tableSize.x)
                {
                    x = 0;
                    y++;
                }
                craftingSlots[j + i * tableSize.x] = slot;
            }
        }

        // 最后一个为输出槽
        CraftingSlot outputSlot = Instantiate(craftingSlotTemplate, craftingTable).GetComponent<CraftingSlot>();
        outputSlot.gameObject.SetActive(true);
        outputSlot.item = null;
        outputSlot.isOutputSlot = true;
        RectTransform outputTrans = outputSlot.rectTransform;
        float outputX = outputTrans.anchoredPosition.x + (int)tableSize.x * tableCellSize;
        float outputY = outputTrans.anchoredPosition.y - (int)(tableSize.y * 0.5f) * tableCellSize;
        outputTrans.anchoredPosition = new Vector2(outputX, outputY);

        outputCraftingSlot = outputSlot;
    }
}