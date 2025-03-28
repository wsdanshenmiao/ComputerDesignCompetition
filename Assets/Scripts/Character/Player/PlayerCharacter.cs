using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character, ITargetable
{
    public PlayerSO templatePlayerPara;
    [HideInInspector] public PlayerSO playerPara;

    private bool canBeTargeted = true;

    public bool CanBeTargeted => canBeTargeted;
    [HideInInspector] public bool isCast = false;

    [HideInInspector] public float currSprintCD;
    
    protected override void Update()
    {
        base.Update();
        currSprintCD -= Time.deltaTime;
        
        if (IsDeath) {
            GameManager.Instance.SetPlayerPosition();
            ResetData();
        }
    }

    public void SetTargetable(bool targetable)
    {
        canBeTargeted = targetable;
    }

    public Transform GetTargetTransform()
    {
        return transform;
    }

    protected override void ResetData()
    {
        base.ResetData();
        playerPara = Instantiate(templatePlayerPara);
        currSprintCD = playerPara.sprintCD;
        canBeTargeted = true;
        isCast = false;
    }

    public override void LoadData(GameData _data)
    {
        base.LoadData(_data);
        CurrHealth = charaPara.maxHealth;

        string maxManaID = "MaxMana" + dataDef.dataID;
        string currManaID = "CurrMana" + dataDef.dataID;
        string manaRestorateID = "ManaRestorate" + dataDef.dataID;
        string jumpHeightID = "JumpHeight" + dataDef.dataID;
        string sprintDurID = "SprintDur" + dataDef.dataID;

        if (_data.floatDatas.ContainsKey(maxManaID))
        {
            playerPara.maxMana = _data.floatDatas[maxManaID];
            playerPara.manaRestorate = _data.floatDatas[manaRestorateID];
            playerPara.jumpHeight = _data.floatDatas[jumpHeightID];
            playerPara.sprintDuration = _data.floatDatas[sprintDurID];
        }

        isDeath = false;
        canMove = true;
        canBeTargeted = true;
    }

    public override void SaveData(ref GameData _data)
    {
        base.SaveData(ref _data);
        // 存档后玩家回血
        CurrHealth = charaPara.maxHealth;

        string maxManaID = "MaxMana" + dataDef.dataID;
        string currManaID = "CurrMana" + dataDef.dataID;
        string manaRestorateID = "ManaRestorate" + dataDef.dataID;
        string jumpHeightID = "JumpHeight" + dataDef.dataID;
        string sprintDurID = "SprintDur" + dataDef.dataID;

        if (_data.floatDatas.ContainsKey(maxManaID))
        {
            _data.floatDatas[maxManaID] = playerPara.maxMana;
            _data.floatDatas[manaRestorateID] = playerPara.manaRestorate;
            _data.floatDatas[jumpHeightID] = playerPara.jumpHeight;
            _data.floatDatas[sprintDurID] = playerPara.sprintDuration;
        }
        else
        {
            _data.floatDatas.Add(maxManaID, playerPara.maxMana);
            _data.floatDatas.Add(manaRestorateID, playerPara.manaRestorate);
            _data.floatDatas.Add(jumpHeightID, playerPara.jumpHeight);
            _data.floatDatas.Add(sprintDurID, playerPara.sprintDuration);
        }
    }
}
