using System;


[Serializable]
public class BackPackItem
{
    public ItemData itemData;
    public int stackSize;

    public BackPackItem(ItemData _newItemData)
    {
        itemData = _newItemData;
        AddStack(1);
    }

    public void AddStack(int _amountToAdd) => stackSize += _amountToAdd;
    public void RemoveStack(int _amountToRemove) => stackSize -= _amountToRemove;
}

