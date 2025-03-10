using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Food,
    potion,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    
    public ItemType type;
    public string itemName;
    public Sprite icon;
    public string itemId;

    protected void OnValidate()
    {
         itemName = this.name;

//用 SO路径 转化为物品ID
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}
