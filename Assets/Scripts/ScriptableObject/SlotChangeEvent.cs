using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 合成物品槽改变时触发
/// </summary>
[CreateAssetMenu(menuName = "Event/SlotChangeEvent")]
public class SlotChangeEvent : ScriptableObject
{
    public UnityAction<int, int, ItemScriptableObject> OnEventRaised; // 无参事件

    // 调用事件函数
    public void RaiseEvent(int x, int y, ItemScriptableObject item)
    {
        OnEventRaised?.Invoke(x, y, item); // 启动
    }
}