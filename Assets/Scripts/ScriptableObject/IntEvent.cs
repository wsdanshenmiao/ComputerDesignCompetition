using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/IntEventSO")]
public class IntEvent : ScriptableObject
{
    public UnityAction<int> OnEventRaised; // 无参事件

    // 调用事件函数
    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value); // 启动
    }
}