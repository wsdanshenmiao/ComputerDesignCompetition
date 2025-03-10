/****************************************************************************************
	Author:			Crusher
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       管理所有场景加载的场景加载器
****************************************************************************************/
/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.15
	Finish time:	
	Abstract:       添加场景的保存
****************************************************************************************/

using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    public SavePriority LoadPriority => SavePriority.CoreSystem;
    [Header("事件")]
    public ExitSceneEventSO ExitSceneEvent;
    public SceneLoadEventSO SceneLoadEvent;
    public VoidEventSO AfterSceneLoadEvent;
    public VoidEventSO NewGameEvent; // 开始新游戏事件
    public FadeEventSO FadeEvent;

    [Header("场景")]
    public GameSceneSO MainMenuScene; // 主菜单场景
    public GameSceneSO FirstScene; // 主菜单后的第一个场景
    private GameSceneSO CurrentLoadScene; // 当前的场景
    private GameSceneSO SceneToLoad; // 下一个要加载的场景
    private GameSceneSO LastScene; // 上一个场景

    [Header("位置参数")]
    public Vector3 firstPos; // 开始后玩家的初始位置
    private Vector3 lastPos; // 玩家进入特殊场景的之前的位置
    private Vector3 positionToGo; // 要加载场景中人物的位置

    [Header("淡入淡出参量")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool isFadeScreen;

    [Header("淡入淡出画布")]
    public Transform fadeCanvas;

    private bool isLoading; // 是否正在加载,默认时未加载
    [SerializeField] private Transform PlayerTrans; // 获取玩家组件的位置


    private void Start()
    {
        // 开始时载入菜单界面(Awake太快,不用)
        SceneLoadEvent.RaiseEvent(MainMenuScene, firstPos, isFadeScreen);
    }

    private void OnEnable()
    {
        //ISaveable saveable = this;
        //saveable.RegisterSaveData();

        // 注册
        NewGameEvent.OnEventRaised += OnNewGame;
        SceneLoadEvent.OnEventRaised += RequestLoadEvent;
        ExitSceneEvent.OnEventRaised += ExitScene;
    }

    private void OnDisable()
    {
        //ISaveable saveable = this;
        //saveable.UnRegitsterSaveData();

        SceneLoadEvent.OnEventRaised -= RequestLoadEvent;
        NewGameEvent.OnEventRaised -= OnNewGame;
        ExitSceneEvent.OnEventRaised -= ExitScene;
    }

    // 新游戏事件在开始按钮处调用
    private void OnNewGame()
    {
        SceneToLoad = FirstScene;
        SceneLoadEvent.RaiseEvent(SceneToLoad, firstPos, true); // 启动事件
    }

    // 通过按钮触发离开事件,返回之前的场景
    private void ExitScene()
    {
        if (isLoading)
        {
            return;
        }
        isLoading = true;
        SceneToLoad = LastScene; // 准备加载之前的场景
        positionToGo = lastPos; // 回到前的位置

        // 携程
        StartCoroutine(UnLoadPreviousScene());
    }

    private void RequestLoadEvent(GameSceneSO LoadScene, Vector3 Pos, bool FadeOrNot)
    {
        // 如果场景还在加载中,就不能再次进入这个方法
        if (isLoading)
        {
            return;
        }
        isLoading = true; // 开始加载操作


        if (CurrentLoadScene != null)
        {
            LastScene = CurrentLoadScene;
            lastPos = Pos;
        }

        SceneToLoad = LoadScene;
        positionToGo = Pos;
        isFadeScreen = FadeOrNot;
        StartCoroutine(UnLoadPreviousScene()); // 开始携程
    }

    private IEnumerator UnLoadPreviousScene()
    {
        // 如果要加载的场景不是菜单类的,就渐入
        if (isFadeScreen && SceneToLoad.SceneType != SceneType.Menu)
        {
            // 实现淡入淡出
            FadeEvent.FadeIn(fadeDuration);
            yield return new WaitForSeconds(fadeDuration); // 当完全渐入后,开始卸载之前的场景
        }

        // 如果不为进入主菜单的场景
        if (CurrentLoadScene != null)
        {
            yield return CurrentLoadScene.sceneReference.UnLoadScene(); // 卸载场景
            PlayerTrans.gameObject.SetActive(false); // 隐藏人物
        }
        // 只要是加载场景,都把人物隐藏了
        PlayerTrans.gameObject.SetActive(false); // 隐藏人物

        UIManager.Instance.OnCloseCanvas();
        // 完成淡入和卸载后加载新场景
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        // 固定操作:加载场景
        var loadingNewScene = SceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingNewScene.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        CurrentLoadScene = SceneToLoad; // 此时的场景就时切换后的场景
        // 淡出
        if (isFadeScreen && SceneToLoad.SceneType != SceneType.Menu)
        {
            FadeEvent.FadeOut(fadeDuration); // 不需要携程
        }

        if (CurrentLoadScene.SceneType == SceneType.Location)
        {
            PlayerTrans.position = positionToGo;

            PlayerTrans.gameObject.SetActive(true); // 只有是地点场景才会显示人物

            // 只有Location类型的才需要显示Canvas
            UIManager.Instance.OnOpenCanvas();
        }

        isLoading = false; // 加载结束

        // 如果不是菜单类型和特殊类型就不需要获得摄像机边界
        if (CurrentLoadScene.SceneType == SceneType.Location)
        {
            AfterSceneLoadEvent.RaiseEvent();
        }
    }


    public bool OnFadeFinish()
    {
        if (fadeCanvas.GetComponentInChildren<Image>().color.a <= 0.01f)
        {
            // 已完成
            return true;
        }
        return false;
    }

    public bool LoadData(GameData _data)
    {
        string playerID = PlayerController.Instance.GetComponent<DataDefiantion>().dataID;
        string posXID = "PosX" + playerID;
        string posYID = "PosY" + playerID;
        string posZID = "PosZ" + playerID;
        if (_data.floatDatas.ContainsKey(posXID))
        {
            Vector3 pos;
            pos.x = _data.floatDatas[posXID];
            pos.y = _data.floatDatas[posYID];
            pos.z = _data.floatDatas[posZID];

            SceneToLoad = _data.GetScene();
            if (SceneToLoad != null)
            {
                RequestLoadEvent(SceneToLoad, pos, true);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool SaveData(ref GameData _data)
    {
        if (CurrentLoadScene != null && CurrentLoadScene.SceneType != SceneType.Menu)
        {
            _data.SaveScene(CurrentLoadScene);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetFadeOrNot()
    {
        return isFadeScreen;
    }
}