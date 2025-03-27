using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, ISaveable
{
    [SerializeField] private IntEvent OnDialogEndEvent;
    
    public SavePriority LoadPriority => SavePriority.CoreSystem;

    [SerializeField] private SavePointSO[] checkPoints;
    private string lastCheckpointId;
    
    // 当前所在的场景
    public static int currSceneIndex = 1;
    // 当前锁住的关卡
    public static bool[] lockScene= new bool[3]{true,true,true};
    

    [SerializeField] private int[] portalDialogIndexs;
    [SerializeField] private GameSceneSO portalScene;

    private void OnEnable()
    {
        OnDialogEndEvent.OnEventRaised += OnDialogEnd;
    }

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
        //StartCoroutine(PlayBGM(AudioName.BGM1, AudioName.BGM2));
    }
    
    private IEnumerator PlayBGM(string BGM1, string BGM2)
    {
        AudioManager.PlayAudio(BGM1);
        float clipLength = AudioManager.GetAudioSource(BGM1).clip.length;
        yield return new WaitForSeconds(clipLength);
        PlayBGM(BGM2, BGM1);
    }
    
    public void LoadData(GameData _data)//获取上次存档点，并将玩家坐标设置到此处
    {
        // ActiveCheckpoint(_data);

        lastCheckpointId = _data.lastCheckPoint;
        //Invoke("SetPlayerPosition", .1f);
    }

    private void SetPlayerPosition()//将玩家位置设置到最近一次的存档点处
    {
        //TODO 把存档点绑定到checkPoints里面
        foreach (SavePointSO savePointData in checkPoints)
        {
            if (savePointData.savePointId == lastCheckpointId)
            {
                PlayerController.Instance.transform.position = savePointData.pos;
            }
        }
    }

    public void SaveData(ref GameData _data)//储存最近一次的存档地点,以及已激活的存档点
    {
        _data.chekcpoints.Clear();

        _data.lastCheckPoint = lastCheckpointId;

        foreach (SavePointSO savePoint in checkPoints)
        {
            _data.chekcpoints.Add(savePoint.savePointId, savePoint.isActivated);
        }
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void UpdateLastCheckpointId(string newCheckpointId)
    {
        lastCheckpointId = newCheckpointId;
    }

}
