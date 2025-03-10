
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public SceneLoadEventSO SceneLoadEvent;
    public ExitSceneEventSO SceneExitEvent;
    // [SerializeField]private GameSceneSO SceneToLoad;
    // [SerializeField]private Vector3 PosToGo;

    /// <summary>
    /// 点击传送点方法,当点击传送点时,画面变黑,然后将人物传送至指定场景,同时点击传送时,也要给我们一个场景
    /// </summary>
    public void PortalToNew(GameSceneSO SceneToLoad, Vector3 PosToGo)
    {
        SceneLoadEvent.RaiseEvent(SceneToLoad, PosToGo, SceneLoader.Instance.GetFadeOrNot());
    }

    public void ExitScene()
    {
        if (SceneLoader.Instance.OnFadeFinish() == true)
        {
            SceneExitEvent.RaiseEvent();
        }
    }
}
