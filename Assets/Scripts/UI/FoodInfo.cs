using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodInfo : MonoBehaviour
{
    public Sprite FoodSprite;
    public FoodType foodType;
    private void Update()
    {
        // 为空获取
        if (FoodSprite == null)
        {
            GetFoodSprite();
        }
    }

    public void GetFoodSprite()
    {
        FoodSprite = GetComponent<Image>().sprite;
    }
}
