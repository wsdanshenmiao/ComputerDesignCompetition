using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private IntEvent OnDialogEndEvent;

    [SerializeField] private SavePointSO[] checkPoints;
    private string lastCheckpointId;
    
    // 当前所在的场景
    public static int currSceneIndex = 0;
    // 当前锁住的关卡
    public static bool[] lockScene= new bool[3]{true,true,true};
    

    [SerializeField] private int[] portalDialogIndexs;
    [SerializeField] private GameSceneSO portalScene;

    private void OnEnable()
    {
        OnDialogEndEvent.OnEventRaised += OnDialogEnd;
    }

    /// <summary>
    /// 通关一关之后传送到选图地点
    /// </summary>
    /// <param name="dialogIndex"></param>
    private void OnDialogEnd(int dialogIndex)
    {
        // 若是结尾的对话则传送到下一关
        if (Array.Exists(portalDialogIndexs, x => x == dialogIndex)) {
            SceneManager.Instance.PortalToNew(portalScene, Vector3.zero);
        }
    }

    private void OnDisable()
    {
        OnDialogEndEvent.OnEventRaised -= OnDialogEnd;
    }

    public void Start()
    { 
        AudioManager.PlayAudio(AudioName.BGM1);
    }
    
    private IEnumerator PlayBGM(string BGM1, string BGM2)
    {
        AudioManager.PlayAudio(BGM1);
        float clipLength = AudioManager.GetAudioSource(BGM1).clip.length;
        yield return new WaitForSeconds(clipLength);
        PlayBGM(BGM2, BGM1);
    }
    
    public void SetPlayerPosition()//将玩家位置设置到最近一次的存档点处
    {
        foreach (SavePointSO savePointData in checkPoints)
        {
            if (savePointData.savePointId == lastCheckpointId)
            {
                PlayerController.Instance.transform.position = savePointData.pos;
            }
        }
    }

    public void PauseGame(bool _pause)
    {
        /*if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;*/
    }

    public void UpdateLastCheckpointId(string newCheckpointId)
    {
        lastCheckpointId = newCheckpointId;
    }

}
