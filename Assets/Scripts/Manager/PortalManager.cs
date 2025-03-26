/****************************************************************************************
	Author:			Crusher
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       用于管理传送系统的Manager
****************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : Singleton<PortalManager>
{
    [Header("传送列表")]
    public List<PortailInfo> portailInfos = new List<PortailInfo>();

    [Header("基础信息列表")]
    public List<GameSceneSO> gameScenes = new List<GameSceneSO>();
    public List<Vector3> postions = new List<Vector3>();

    private PortailInfo currentInfo;

    protected override void Awake()
    {
        DistributeInfo();
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
        currentInfo = portailInfo;
        StartPortal();
    }

    public void StartPortal()
    {
        Debug.unityLogger.Log("StartPortal");
        SceneManager.Instance.PortalToNew(currentInfo.GameScene, currentInfo.Position);
    }
}
