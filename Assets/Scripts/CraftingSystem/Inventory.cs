
using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> itemList;
    public event EventHandler OnItemListChanged;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        // 可堆叠
        bool hasItem = false;
        foreach (Item inventoryItem in itemList)
        {
            if (item.itemScriptableObject.itemType ==
                inventoryItem.itemScriptableObject.itemType)
            {
                inventoryItem.amount++;
                hasItem = true;
                break;
            }
        }
        // 没有则新建
        if (!hasItem)
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.amount > 1)
        {
            item.amount--;
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItems()
    {
        return itemList;
    }
}
