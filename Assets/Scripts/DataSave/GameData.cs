using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string sceneToSave;

    public SerializableDictionary<string, float> floatDatas;

    public SerializableDictionary<string, int> inventory;

    public SerializableDictionary<string, bool> chekcpoints;
    public string lastCheckPoint;
    //储存食材状态
    public SerializableDictionary<string, bool> Ingredients;
    //储存钥匙状态
    public SerializableDictionary<string, bool> Keys;
    //储存血瓶状态  
    public SerializableDictionary<string, bool> HealthPotion;
    //储存藤蔓状态
    public SerializableDictionary<string, bool> Vine;
    //储存锁状态
    public SerializableDictionary<string, bool> lockStates;
    //储存锁收集的钥匙状态
    public SerializableDictionary<string, SerializableHashSet<string>> lockCollectedKeys;

    // 添加用于存储已解锁技能的字段
    public SerializableHashSet<string> unlockedSkills;

    public GameData()
    {
        floatDatas = new SerializableDictionary<string, float>();

        inventory = new SerializableDictionary<string, int>();

        chekcpoints = new SerializableDictionary<string, bool>();
        lastCheckPoint = string.Empty;

        Ingredients = new SerializableDictionary<string, bool>();
        Keys = new SerializableDictionary<string, bool>();
        HealthPotion = new SerializableDictionary<string, bool>();
        Vine = new SerializableDictionary<string, bool>();

        // 使用SerializableDictionary初始化
        lockStates = new SerializableDictionary<string, bool>();
        lockCollectedKeys = new SerializableDictionary<string, SerializableHashSet<string>>();
        // 初始化技能集合
        unlockedSkills = new SerializableHashSet<string>(new List<string>());
    }


    public void SaveScene(GameSceneSO gameScene)
    {
        sceneToSave = JsonUtility.ToJson(gameScene);
    }

    public GameSceneSO GetScene()
    {
        GameSceneSO ret = ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave, ret);
        return ret;
    }

}