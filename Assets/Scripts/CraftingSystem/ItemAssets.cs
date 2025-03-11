using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemAssets : Singleton<ItemAssets>
{
    public Transform itemPrefab;
    
    
    public Sprite magnetSprite;
    public Sprite metallicCardSprite;
    public Sprite magneticNeedleSprite;

    public RectTransform magnetTargetPos; 
    public RectTransform metallicCardTargetPos;
    public RectTransform magneticNeedleTargetPos;
}