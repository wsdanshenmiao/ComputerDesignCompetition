using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemAssets : Singleton<ItemAssets>
{
    public Transform itemPrefab;
    
    public Sprite magnetSprite;
    public Sprite metallicCardSprite;
    public Sprite magneticNeedleSprite;
    public Sprite bowlSprite;
    public Sprite waterSprite;
    public Sprite waterBowlSprite;
    public Sprite foamSprite;
    public Sprite foamNeedleSprite;
    public Sprite compassSprite;

    public RectTransform magnetTargetPos; 
    public RectTransform metallicCardTargetPos;
    public RectTransform magneticNeedleTargetPos;
}