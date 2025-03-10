using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : InteractableObject, I_Interactable
{
    public SavePointSO savePointData;

    protected override void Start()
    {
        base.Start();
        GenerateId();
    }

    protected void Update()
    {
        savePointData.pos = transform.position;
    }

    protected override void Reset()
    {
        base.Reset();
    }


    [ContextMenu("Generate checkpoint id")]//给存档点分配ID
    private void GenerateId()
    {
        savePointData.savePointId = System.Guid.NewGuid().ToString();
    }

    public void TriggerAction()
    {
        SaveAtCheckpoint();
    }

    private void SaveAtCheckpoint()//存档
    {
        savePointData.isActivated = true;

        GameManager.Instance.UpdateLastCheckpointId(savePointData.savePointId);
        SaveManager.Instance.SaveLastScene();
        SaveManager.Instance.SaveGame();
        // 可以在这里添加存档提示效果
        Debug.Log("SaveData");
    }

}

