using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> OnEventRaised;

    public void RaiseEvent(GameSceneSO SceneToLoad, Vector3 PositionToGo, bool isFade)
    {
        OnEventRaised.Invoke(SceneToLoad, PositionToGo, isFade);
    }
}
