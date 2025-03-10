
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
        
        AddItem(new Item{itemType = Item.ItemType.Magnet, amount = 1});
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("AddItem");
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
