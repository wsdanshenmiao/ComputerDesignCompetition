using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ExitSceneEventSO")]
public class ExitSceneEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }
}
