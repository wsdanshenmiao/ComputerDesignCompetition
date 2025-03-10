using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldownTime; // 冷却时间
    [SerializeField] protected float timeCounter;
    protected bool skillUsed = true;

    protected PlayerController player;


    protected virtual void Start()
    {
        player = PlayerController.Instance;
    }
    protected virtual void Update()
    {
        timeCounter -= Time.deltaTime;
    }

    public virtual bool IsInCooldown()  // 新增方法：检查是否在冷却中
    {
        return timeCounter > 0;
    }

    public virtual void StartCooldown()  // 新增方法：开始冷却
    {
        timeCounter = cooldownTime;
    }

    public virtual void UseSkill()
    {

    }
}
