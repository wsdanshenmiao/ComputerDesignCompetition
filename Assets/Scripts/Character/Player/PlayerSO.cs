using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("运动参数")]
    public float walkSpeed = 5;   // 创建一个速度变量来存储速度
    public float runSpeed = 8;
    public float sprintSpeed = 8;   // 冲刺时的速度
    public float jumpHeight = 3.5f; // 创建一个变量来储存跳跃时施加的力的大小

    [Header("运动时间参数")]
    public float sprintDuration = .7f;    // 冲刺时间
    public float jumpDuration = .5f;      // 跳跃时间
    public float coyoteDuration = .1f;    // 土狼时间

    [Header("玩家基本属性")]
    public float sprintCD = .5f;    // 冲刺的cd时间，小于0可冲刺

    [Header("玩家魔力属性")]
    public float maxMana = 100;     // 法力值
    public float manaCD = .5f;      // 魔法CD
    public float manaDelay = 2;     // 停止释放魔法后延迟回复魔力的时间
    public float manaRestorate = 10;// 魔力回复速度

    [Header("玩家大小")]
    public float originSize = 1;
    public float bigSize = 2;
    public float smallSize = .5f;
}
