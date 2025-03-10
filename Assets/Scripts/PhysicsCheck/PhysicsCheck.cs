using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    [Header("检测状态")]
    [HideInInspector] public bool isGround; // 地面
    [HideInInspector] public bool touchLeftWall; // 左墙壁
    [HideInInspector] public bool touchRightWall; // 右墙壁

    [Header("检测参数")]
    [SerializeField] private Vector2 bottomOffset; // 记录脚底位移差值(当一个物体的锚点不在正下方时,就需要这样调整)
    [SerializeField] private Vector2 leftOffset; // 记录左边的位移差值
    [SerializeField] private Vector2 rightOffset; // 记录右边的位移差值
    [SerializeField] private LayerMask groundLayer; // Ground图层
    [SerializeField] private float checkRadius;

    private void Update()
    {
        Cheack();
    }

    public void Cheack()
    {
        // 检查地面(需要正面位置)
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius, groundLayer); // 如果在半径范围内检测到了地面的图层，就返回isGround
        // layerMask是指图层掩码,而不是索引值
        // 检查左侧墙体
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        // 检查右侧墙体
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制底部虚球
        Gizmos.DrawWireSphere(((Vector2)transform.position) + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius);
        // 绘制左侧虚球
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        // 绘制右侧虚球
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
