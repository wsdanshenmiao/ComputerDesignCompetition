using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, ISaveable
{
    public SavePriority LoadPriority => SavePriority.CoreSystem;

    [SerializeField] private SavePointSO[] checkPoints;
    private string lastCheckpointId;

    public void Start()
    {
        StartCoroutine(PlayBGM(AudioName.BGM1, AudioName.BGM2));
    }

    private IEnumerator PlayBGM(string BGM1, string BGM2)
    {
        AudioManager.PlayAudio(BGM1);
        float clipLength = AudioManager.GetAudioSource(BGM1).clip.length;
        yield return new WaitForSeconds(clipLength);
        PlayBGM(BGM2, BGM1);
    }

    //TODO
    // public void ReStart()//返回存档点
    // {
    //     SaveManager.instance.SaveGame();
    //     Scene scene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(scene.name);
    // }

    // public void SaveAndExit()//保存游戏并返回主菜单
    // {
    //     SaveManager.instance.SaveGame();
    //     SceneManager.LoadScene("MainMenu");
    // }

    public void LoadData(GameData _data)//获取上次存档点，并将玩家坐标设置到此处
    {
        // ActiveCheckpoint(_data);

        lastCheckpointId = _data.lastCheckPoint;
        //Invoke("SetPlayerPosition", .1f);
    }

    // private void ActiveCheckpoint(GameData _data)//激活摸过的存档点的动画
    // {
    //     foreach (Tent checkPoint in checkPoints)
    //     {
    //         foreach (KeyValuePair<string, bool> savedCheckPoint in _data.chekcpoints)
    //         {
    //             if (checkPoint.checkpointId == savedCheckPoint.Key && savedCheckPoint.Value == true)
    //             {
    //                 checkPoint.ActivateCheckpoint();
    //             }

    //         }
    //     }
    // }

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
