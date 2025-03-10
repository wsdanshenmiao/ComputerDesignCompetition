using UnityEngine;
using System.Collections.Generic;
public class Lock : MonoBehaviour, ISaveable
{
    [SerializeField] private List<string> requiredKeyIDs;  // 需要的钥匙ID列表
    [SerializeField] private string lockID; // 锁的唯一标识符
    private HashSet<string> collectedKeyIDs = new HashSet<string>();  // 已收集的钥匙ID
    private bool isUnlocked = false;

    public SavePriority LoadPriority => SavePriority.Environment;

    private void OnEnable()
    {
        // 订阅钥匙收集事件
        Key.OnKeyCollected += OnKeyCollected;
    }

    private void OnDisable()
    {
        // 取消订阅钥匙收集事件
        Key.OnKeyCollected -= OnKeyCollected;
    }

    private void Start()
    {
        // 如果没有设置lockID，自动生成一个
        if (string.IsNullOrEmpty(lockID))
        {
            GenerateLockID();
        }
    }

    [ContextMenu("Generate Lock ID")]
    private void GenerateLockID()
    {
        lockID = System.Guid.NewGuid().ToString();
    }

    private void OnKeyCollected(string collectedKeyID)
    {
        // 检查收集的钥匙ID是否在需求列表中
        if (!isUnlocked && requiredKeyIDs.Contains(collectedKeyID))
        {
            collectedKeyIDs.Add(collectedKeyID);

            // 检查是否收集齐所有钥匙
            if (collectedKeyIDs.Count == requiredKeyIDs.Count)
            {
                isUnlocked = true;
                gameObject.SetActive(false); 
                AudioManager.PlayAudio(AudioName.PasswordTileDisappeared);
            }
        }
    }

    public void LoadData(GameData data)
    {
        // 如果锁的状态存在于存档中且为true（已解锁），则销毁锁物体
        if (data.lockStates.TryGetValue(lockID, out bool unlocked) && unlocked)
        {
            isUnlocked = true;
            Destroy(gameObject);
        }
        else if (data.lockCollectedKeys.TryGetValue(lockID, out SerializableHashSet<string> savedCollectedKeys))
        {
            // 恢复已收集的钥匙状态
            collectedKeyIDs = new HashSet<string>(savedCollectedKeys);
        }
    }

    public void SaveData(ref GameData data)
    {
        // 保存锁的解锁状态
        if (data.lockStates.ContainsKey(lockID))
        {
            data.lockStates[lockID] = isUnlocked;
        }
        else
        {
            data.lockStates.Add(lockID, isUnlocked);
        }

        // 保存已收集的钥匙状态
        if (data.lockCollectedKeys.ContainsKey(lockID))
        {
            data.lockCollectedKeys[lockID] = new SerializableHashSet<string>(collectedKeyIDs);
        }
        else
        {
            data.lockCollectedKeys.Add(lockID, new SerializableHashSet<string>(collectedKeyIDs));
        }
    }
}
