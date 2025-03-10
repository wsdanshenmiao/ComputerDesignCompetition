/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的属性
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("运动参数")]
    public float portalSpeed = 3; // 巡逻速度
    public float chaseSpeed = 6; // 追击速度

    [Header("检测范围")]
    public float checkRange = 20;

    public float idleTime = 3;      // 待机时间
    public float stopDistance = 2;  // 距离玩家停止的距离
}
