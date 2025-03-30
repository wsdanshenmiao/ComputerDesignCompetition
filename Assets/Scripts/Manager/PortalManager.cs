using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : Singleton<PortalManager>
{
    [SerializeField] private VoidEventSO AfterSceneLoadEvent;
    
    [Header("传送列表")]
    public List<PortailInfo> portailInfos = new List<PortailInfo>();

    [Header("基础信息列表")]
    public List<GameSceneSO> gameScenes = new List<GameSceneSO>();
    public List<Vector3> postions = new List<Vector3>();

    private PortailInfo currentInfo;

    // 防止重复点击
    private bool isPortal = false;

    protected override void Awake()
    {
        DistributeInfo();
    }

    protected void OnEnable()
    {
        AfterSceneLoadEvent.OnEventRaised += AfterSceneLoad;
    }

    protected void OnDisable()
    {
        AfterSceneLoadEvent.OnEventRaised -= AfterSceneLoad;
    }

    private void AfterSceneLoad()
    {
        isPortal = false;
    }

    public void DistributeInfo()
    {
        for (int i = 0; i < portailInfos.Count; i++)
        {
            portailInfos[i].GameScene = gameScenes[i];
            portailInfos[i].Position = postions[i];
        }
    }

    // 选中方法
    public void ChoosePoint(PortailInfo portailInfo)
    {
        // 检测场景是否解锁
        if (!isPortal && !GameManager.lockScene[portailInfo.sceneIndex]) {
            isPortal = true;
            currentInfo = portailInfo;
            StartPortal();
        }
    }

    public void StartPortal()
    {
        Debug.Log("StartPortal");
        SceneManager.Instance.PortalToNew(currentInfo.GameScene, currentInfo.Position);
    }
}
