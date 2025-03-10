using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [Header("基本属性")]
    public float maxHealth = 100;
    public float invalidTime = 0.1f;    // 无敌帧,小于0则可攻击
    public float hurtStopTime = .5f;// 受伤后的僵直时间

    [Header("攻击的各项属性")]
    public int damage = 20;             // 攻击力
    public float attackRange = 25;      // 攻击范围
    public float attackRate = .5f;      // 攻击频率
    public float knockFactor = 2;       // 击退系数
    public float knockDuring = .3f;     // 击退持续时间
}
