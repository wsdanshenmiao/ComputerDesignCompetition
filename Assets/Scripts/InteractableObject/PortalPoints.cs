using UnityEngine;

public class PortalPoints : InteractableObject, I_Interactable
{
    [Header("事件")]
    public SceneLoadEventSO SceneLoadEvent;
    public VoidEventSO AfterSceneLoadEvent;

    [Header("传送参数")]
    public GameSceneSO SceneToLoad;

    private bool canInvoke;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    public void TriggerAction()
    {
        {
            // 当触发水晶球时,就会触发传送事件
            SceneLoadEvent.RaiseEvent(
                SceneToLoad,
                transform.position,
                SceneLoader.Instance.GetFadeOrNot());
        }
    }
}
