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
    [Header("返回场景事件")]
    public ExitSceneEventSO ExitSceneEvent;

    [Header("传送点名称文本文件")]
    public TextAsset pointNameText;

    [Header("描述文本文件")]
    public TextAsset descriptionText;

    [Header("传送列表")]
    public List<PortailInfo> portailInfos = new List<PortailInfo>();

    [Header("基础信息列表")]
    public List<GameSceneSO> gameScenes = new List<GameSceneSO>();
    public List<Vector3> postions = new List<Vector3>();
    public List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private string[] portalNames = new string[10]; // 自动根据文本分配
    private string[] descriptions = new string[10]; // 自动根据文本分配

    [Header("环境对象")]
    public GameObject Environment;

    [Header("描述文本对象")]
    public GameObject Description;

    private PortailInfo currentInfo;

    protected override void Awake()
    {
        ReadTextData();
        DistributeInfo();
    }

    public void DistributeInfo()
    {
        for (int i = 0; i < portailInfos.Count; i++)
        {
            portailInfos[i].GameScene = gameScenes[i];
            portailInfos[i].Position = postions[i];
            portailInfos[i].Sprite = sprites[i];
            portailInfos[i].Description = descriptions[i];
            portailInfos[i].PortalName = portalNames[i];
        }
    }

    public void ReadTextData()
    {
        portalNames = pointNameText.text.Split('\n'); // 分配传送点的名字
        descriptions = descriptionText.text.Split('\n'); // 分配描述
    }

    // 选中方法
    public void ChoosePoint(PortailInfo portailInfo)
    {
        currentInfo = portailInfo;
        Description.GetComponent<TMP_Text>().text = portailInfo.Description;
        Environment.GetComponent<Image>().sprite = portailInfo.Sprite;
    }

    public void StartPortal()
    {
        SceneManager.Instance.PortalToNew(currentInfo.GameScene, currentInfo.Position);
    }
}
