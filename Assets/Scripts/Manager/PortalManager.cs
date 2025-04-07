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
    
    // 在通关某一关后解锁对应的图片
    [SerializeField] private GameObject[] imageArray;

    protected override void Awake()
    {
        DistributeInfo();
    }

    protected void Start()
    {
        ActivateImage();
    }

    private void ActivateImage()
    {
        //加载本场景后，更新过关图片的显示
        for (int i = 0; i < portailInfos.Count; i++)
        {
            //DEBUG
            // if (GameManager.lockScene[i] == true)
            // {
            //     Debug.Log("Level " + i + " is locked");
            // }
            // else
            // {
            //     Debug.Log("Level " + i + " is not locked");
            // }

            //如果当前关卡处于锁的状态，则解锁的关卡在后面，所以当前关一定为已通关的状态
            if (GameManager.lockScene[i])
            {
                if (i >= 0 && i < imageArray.Length)
                {
                    imageArray[i].SetActive(true);
                }
            }
            else //当前关处于解锁的状态，则还没进入，不要加载过关图标
            {
                break; //后面的关卡更加不可能需要加载过关图标
            }
        }
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
        //Debug.Log("StartPortal");
        SceneManager.Instance.PortalToNew(currentInfo.GameScene, currentInfo.Position);
    }
}
