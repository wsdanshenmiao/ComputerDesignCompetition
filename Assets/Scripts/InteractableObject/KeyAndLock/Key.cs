using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(DataDefiantion))]
public class Key : MonoBehaviour, I_Interactable, ISaveable
{
    [SerializeField] private string keyID;  // 用于匹配对应的锁
    [SerializeField] private string keyDescription;  // 可选：添加钥匙描述，方便在Unity编辑器中识别
    public string KeyID => keyID;  // 只读属性
    private DataDefiantion dataDef;
    private bool isCollected = false;

    public SavePriority LoadPriority => SavePriority.Environment;

    public static event System.Action<string> OnKeyCollected;  // 钥匙被收集时触发的事件

    private void Start()
    {
        dataDef = GetComponent<DataDefiantion>();
        
        // 添加 TilemapCollider2D 组件（如果不存在）
        TilemapCollider2D collider = gameObject.GetComponent<TilemapCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<TilemapCollider2D>();
        }
        
        // 设置 trigger 为 true
        collider.isTrigger = true;
        
        // 设置标签为 Interactable
        gameObject.tag = "Interactable";
    }

    private void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    public void TriggerAction()
    {
        if (!isCollected)
        {
            // 触发钥匙收集事件
            OnKeyCollected?.Invoke(keyID);
            isCollected = true;
            // 销毁钥匙物体
            gameObject.SetActive(false); 
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.Keys.ContainsKey(dataDef.dataID))
        {
            isCollected = _data.Keys[dataDef.dataID];
            if (isCollected)
            {
                gameObject.SetActive(false); 
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.Keys.ContainsKey(dataDef.dataID))
        {
            _data.Keys[dataDef.dataID] = isCollected;
        }
        else
        {
            _data.Keys.Add(dataDef.dataID, isCollected);
        }
    }
}
