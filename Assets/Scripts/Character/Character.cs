/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.11
	Abstract:       管理角色共有属性
****************************************************************************************/
/****************************************************************************************
	Author:			danshenmiao
	Versions:		2.0
	Creation time:	2025.1.15
	Finish time:	
	Abstract:       加入数据保存
****************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public CharacterSO templateCharacterPara;
    [HideInInspector] public CharacterSO charaPara;

    [Header("事件广播")]
    public UnityEvent<Character> OnHealthChangeEvent;
    public UnityEvent<Transform> OnHurtEvent;
    public UnityEvent OnDieEvent;

    protected DataDefiantion dataDef;

    protected float currHealth;
    protected float currInvalidTime;
    protected float currKnockTime;

    protected bool isHurt = false;
    protected bool isDeath = false;
    [HideInInspector] public bool canMove = true;

    public SavePriority LoadPriority => SavePriority.PlayerData;

    #region
    public float CurrHealth
    {
        get { return currHealth; }
        set { currHealth = Mathf.Max(0, value); OnHealthChangeEvent.Invoke(this); }
    }

    public bool IsHurt
    {
        get => isHurt; set => isHurt = value;
    }

    public bool IsDeath
    {
        get => isDeath; set => isDeath = value;
    }
    #endregion

    protected virtual void Awake()
    {
        ResetData();
        dataDef = GetComponent<DataDefiantion>();
        Attack attack = GetComponent<Attack>();
        attack?.SetAttackData(charaPara.damage, charaPara.attackRate, charaPara.knockFactor);
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        currInvalidTime -= Time.deltaTime;
    }

    protected virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 重置角色属性
    /// </summary>
    protected virtual void ResetData()
    {
        charaPara = Instantiate(templateCharacterPara);
        CurrHealth = charaPara.maxHealth;
        currInvalidTime = charaPara.invalidTime;
        currKnockTime = 0;
        isHurt = false;
        isDeath = false;
        canMove = true;
    }

    /// <summary>
    /// 伤害函数
    /// </summary>
    /// <param name="attacker"></param>
    public void TakeDamege(Attack attacker)
    {
        // 无敌帧
        if (attacker == null || currInvalidTime > 0) return;
        currInvalidTime = charaPara.invalidTime;

        float damege = attacker.GetDamage();
        if (damege == 0) return;
        CurrHealth = Mathf.Max(0, CurrHealth - damege);
        OnHealthChangeEvent?.Invoke(this);  // 广播受伤事件
        if (CurrHealth <= 0)
        {
            isHurt = true;
            isDeath = true;
            OnDieEvent?.Invoke();
        }
        else
        {
            isHurt = true;
            OnHurtEvent?.Invoke(transform);
        }

        // 进行击退
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 dir = transform.position - attacker.transform.position;
            if (gameObject.activeSelf)
            {
                StartCoroutine(KnockBack(rb, dir, attacker.GetKnockFactor()));
            }
        }
    }

    private IEnumerator ReserveIsHurt()
    {
        yield return new WaitForSeconds(charaPara.hurtStopTime);
        isHurt = false;
    }

    IEnumerator KnockBack(Rigidbody2D rb, Vector3 dir, float force)
    {
        currKnockTime = charaPara.knockDuring;
        canMove = false;

        while (currKnockTime > 0)
        {
            currKnockTime -= Time.deltaTime;
            rb.velocity = dir.normalized * force;
            yield return new WaitForFixedUpdate();
        }

        canMove = true;
    }

    public virtual void LoadData(GameData _data)
    {
        string maxHPID = "MaxHealth" + dataDef.dataID;
        string currHPID = "CurrHP" + dataDef.dataID;
        string posXID = "PosX" + dataDef.dataID;
        string posYID = "PosY" + dataDef.dataID;
        string posZID = "PosZ" + dataDef.dataID;
        Vector3 pos;

        if (_data.floatDatas.ContainsKey(maxHPID))
        {
            charaPara.maxHealth = _data.floatDatas[maxHPID];
            CurrHealth = _data.floatDatas[currHPID];
            pos.x = _data.floatDatas[posXID];
            pos.y = _data.floatDatas[posYID];
            pos.z = _data.floatDatas[posZID];
            transform.position = pos;
            if (CurrHealth > 0)
            {
                IsDeath = false;
            }
            else
            {
                IsDeath = true;
            }
        }
    }

    public virtual void SaveData(ref GameData _data)
    {
        string maxHPID = "MaxHealth" + dataDef.dataID;
        string currHPID = "CurrHP" + dataDef.dataID;
        string posXID = "PosX" + dataDef.dataID;
        string posYID = "PosY" + dataDef.dataID;
        string posZID = "PosZ" + dataDef.dataID;
        Vector3 pos = transform.position;

        if (_data.floatDatas.ContainsKey(maxHPID))
        {
            _data.floatDatas[maxHPID] = charaPara.maxHealth;
            _data.floatDatas[currHPID] = CurrHealth;
            _data.floatDatas[posXID] = pos.x;
            _data.floatDatas[posYID] = pos.y;
            _data.floatDatas[posZID] = pos.z;
        }
        else
        {
            _data.floatDatas.Add(maxHPID, charaPara.maxHealth);
            _data.floatDatas.Add(currHPID, CurrHealth);
            _data.floatDatas.Add(posXID, pos.x);
            _data.floatDatas.Add(posYID, pos.y);
            _data.floatDatas.Add(posZID, pos.z);
        }
    }
}
