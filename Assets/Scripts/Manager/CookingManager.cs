using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingManager : Singleton<CookingManager>
{
    [Header("返回场景事件")]
    public ExitSceneEventSO ExitSceneEvent;

    [Header("食材清单")]
    public List<Sprite> MaterialList = new List<Sprite>(); // 0是berry;1是fruit;2是mushroom

    [Header("食物清单")]
    public List<Sprite> FoodList = new List<Sprite>();

    [Header("食谱清单")]
    public List<GameObject> RecipeList = new List<GameObject>();

    // 链表
    public LinkedList<GameObject> MaterialObjs = new LinkedList<GameObject>();
    public LinkedList<GameObject> FoodObjs = new LinkedList<GameObject>();

    [Header("清单对像Transform")]
    public Transform MaterialTarget;
    private int indexOfMaterial;
    public Transform FoodTarget;
    private int indexOfFood;
    protected override void Awake()
    {
        base.Awake();
        indexOfMaterial = 0;
    }

    protected void Update()
    {
        GameObject materialList = GameObject.Find("MaterialList");
        if (materialList != null)
        {
            MaterialTarget = materialList.transform;
        }
        GameObject foodTarget = GameObject.Find("FoodList");
        if (foodTarget != null)
        {
            FoodTarget = foodTarget.transform;
        }
    }

    // 功能1:制造食物并显示
    public void GetFood()
    {

        int Count = 0;
        foreach (var childTrans in FoodTarget)
            Count++;
        if (indexOfFood == Count || indexOfMaterial == 0) // 材料索引为0也重新返回
        {
            return;
        }

        int[] Array = new int[MaterialObjs.Count];
        int index = 0;
        // 先看链表内的材料,让他们有序储存在数组中
        foreach (GameObject obj in MaterialObjs)
        {
            Sprite sprite = obj.GetComponent<Image>().sprite;
            // 0是berry;1是fruit;2是mushroom
            if (sprite == MaterialList[0])
            {
                // 是berry
                Array[index] = 1;
            }
            else if (sprite == MaterialList[1])
            {
                // 是fruit
                Array[index] = 10;
            }
            else if (sprite == MaterialList[2])
            {
                // 是mushroom
                Array[index] = 100;
            }
            index++;
        }

        // 求数组和
        int sum = 0;
        foreach (int element in Array)
        {
            sum += element;
        }

        // 获取子对象
        GameObject child = FoodTarget.GetChild(indexOfFood).gameObject;

        switch (sum)
        {
            case 1:
            case 2:
            case 3:
                // 食物1
                child.GetComponent<Image>().sprite = FoodList[0];
                break;
            case 10:
            case 20:
            case 30:
                // 食物2
                child.GetComponent<Image>().sprite = FoodList[1];
                break;
            case 100:
            case 200:
            case 300:
                // 食物3
                child.GetComponent<Image>().sprite = FoodList[2];
                break;
            case 11:
                // 食物4
                child.GetComponent<Image>().sprite = FoodList[3];
                child.GetComponent<FoodInfo>().foodType = FoodType.Clone;
                break;
            case 12:
                // 食物5
                child.GetComponent<Image>().sprite = FoodList[4];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_AAB;
                break;
            case 21:
                // 食物6
                child.GetComponent<Image>().sprite = FoodList[5];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_ABB;
                break;
            case 101:
                // 食物7
                child.GetComponent<Image>().sprite = FoodList[6];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_AC;
                break;
            case 110:
                // 食物8
                child.GetComponent<Image>().sprite = FoodList[7];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_BC;
                break;
            case 102:
                // 食物9
                child.GetComponent<Image>().sprite = FoodList[8];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_AAC;
                break;
            case 120:
                // 食物10
                child.GetComponent<Image>().sprite = FoodList[9];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_BBC;
                break;
            case 111:
                // 食物11
                child.GetComponent<Image>().sprite = FoodList[10];
                child.GetComponent<FoodInfo>().foodType = FoodType.Special_ABC;
                break;
            default:
                // 黑暗料理
                child.GetComponent<Image>().sprite = FoodList[11];
                child.GetComponent<FoodInfo>().foodType = FoodType.Die;
                break;
        }
        indexOfFood++;
        FoodObjs.AddLast(child);

        child.GetComponent<Mask>().showMaskGraphic = true;

        // 特殊处理
        if (sum == 1)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.JumpBoost_I;
        }
        else if (sum == 2)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.JumpBoost_II;

        }
        else if (sum == 3)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.JumpBoost_III;
        }
        else if (sum == 10)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.DashBoost_I;
        }
        else if (sum == 20)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.DashBoost_II;
        }
        else if (sum == 30)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.DashBoost_III;
        }
        else if (sum == 100)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.HealthBoost_I;
        }
        else if (sum == 200)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.HealthBoost_II;
        }
        else if (sum == 300)
        {
            child.GetComponent<FoodInfo>().foodType = FoodType.HealthBoost_III;
        }


        // 清除食材
        ResetFood();
        AddNewRecipe(child);
    }

    // 功能2:添加食谱
    public void AddNewRecipe(GameObject gameObject)
    {
        // 先找对象
        foreach (var element in RecipeList)
        {
            if (element.GetComponent<FoodInfo>().FoodSprite == gameObject.GetComponent<Image>().sprite)
            {
                // 判断是否已经解锁了食谱
                GameObject child = element.transform.GetChild(0).gameObject;
                if (child.activeSelf == true)
                    return;

                // 否则就解锁食谱
                element.GetComponent<Image>().enabled = false;
                child.SetActive(true);
            }
        }
    }

    // 功能三:添加食材(当点击第1,2,3个按键时触发)
    public void AddMaterial(Sprite targetSprite)
    {
        // 判断是否超出范围
        int Count = 0;
        foreach (var childTrans in MaterialTarget)
            Count++;
        if (indexOfMaterial == Count)
        {
            return;
        }

        // 获取子对象
        GameObject child = MaterialTarget.GetChild(indexOfMaterial).gameObject;
        MaterialObjs.AddLast(child);
        indexOfMaterial++;

        // 修改图片
        child.GetComponent<Image>().sprite = targetSprite;
        child.GetComponent<Mask>().showMaskGraphic = true;
    }

    // 功能四:重置食材
    public void ResetFood()
    {
        // 重置遮罩
        foreach (Transform child in MaterialTarget)
        {
            child.GetComponent<Mask>().showMaskGraphic = false;
        }

        // 清除链表信息
        MaterialObjs.Clear();

        // 重置索引
        indexOfMaterial = 0;
    }

    // 功能五:吃食物
    public void EatAll()
    {
        RemoveAllEffects();
        // 获得所有食物的效果
        foreach (GameObject food in FoodObjs)
        {
            // 获取食物的FoodType
            FoodType foodType = food.GetComponent<FoodInfo>().foodType;

            // 创建对应的效果并储存
            IFoodEffect effect = FoodEffectFactory.CreateEffect(foodType);
            if (effect != null)
            {
                effect.ApplyEffect();
            }
        }

        // 退出场景
        SceneManager.Instance.ExitScene();
    }

    // 移除所有效果
    public void RemoveAllEffects()
    {
        foreach (FoodType foodType in Enum.GetValues(typeof(FoodType)))
        {
            IFoodEffect effect = FoodEffectFactory.CreateEffect(foodType);
            if (effect != null)
                effect.RemoveEffect();
        }
    }

    public void ExitScene()
    {
        SceneManager.Instance.ExitScene();
    }


}
