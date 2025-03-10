using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRaised; // 无参事件

    // 调用事件函数
    public void RaiseEvent()
    {
        OnEventRaised?.Invoke(); // 启动
    }
}
