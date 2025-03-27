/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.11
	Abstract:       管理玩家的属性
****************************************************************************************/
/****************************************************************************************
	Author:			EFA
	Versions:		2.0
	Creation time:	2025.1.13
	Finish time:	2025.1.13
	Abstract:       添加魔力变化的事件广播和能否被设为目标
****************************************************************************************/

using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character, ITargetable
{
    [Header("事件广播")]
    public UnityEvent<Character> OnManaChange;

    public PlayerSO templatePlayerPara;
    [HideInInspector] public PlayerSO playerPara;

    private bool canBeTargeted = true;

    public bool CanBeTargeted => canBeTargeted;
    [HideInInspector] public bool isCast = false;

    [HideInInspector] public float currSprintCD;

    private float currMana;
    private float currManaDelay;

    #region 玩家数据读取及获取
    public float CurrMana
    {
        get { return currMana; }
        set { currMana = MathF.Max(0, value); OnManaChange.Invoke(this); }
    }
    #endregion

    protected override void Update()
    {
        base.Update();
        currSprintCD -= Time.deltaTime;
        UpdateMana();
    }

    private void UpdateMana()
    {
        currManaDelay = isCast ? playerPara.manaDelay : (currManaDelay - Time.deltaTime);
        if (currManaDelay < 0)
        {
            CurrMana += playerPara.manaRestorate * Time.deltaTime;
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
        CurrMana = playerPara.maxMana;
        currManaDelay = playerPara.manaDelay;
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
            CurrMana = _data.floatDatas[currManaID];
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
            _data.floatDatas[currManaID] = CurrMana;
            _data.floatDatas[manaRestorateID] = playerPara.manaRestorate;
            _data.floatDatas[jumpHeightID] = playerPara.jumpHeight;
            _data.floatDatas[sprintDurID] = playerPara.sprintDuration;
        }
        else
        {
            _data.floatDatas.Add(maxManaID, playerPara.maxMana);
            _data.floatDatas.Add(currManaID, CurrMana);
            _data.floatDatas.Add(manaRestorateID, playerPara.manaRestorate);
            _data.floatDatas.Add(jumpHeightID, playerPara.jumpHeight);
            _data.floatDatas.Add(sprintDurID, playerPara.sprintDuration);
        }
    }
}
