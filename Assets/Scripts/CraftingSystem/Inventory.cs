
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;
    public event EventHandler OnItemListChanged; 

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
