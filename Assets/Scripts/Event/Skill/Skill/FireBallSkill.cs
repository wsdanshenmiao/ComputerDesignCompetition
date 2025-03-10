using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : Skill
{
    [Header("火球配置参数")]
    [SerializeField] private float speed = 10f;        // 飞行速度
    [SerializeField] private float maxRange = 25f;     // 最大射程
    [SerializeField] private int damage = 20;          // 伤害值
    [SerializeField] private GameObject fireBallPrefab; // 火球预制件

    public override void UseSkill()
    {
        //TODO(audio) 增加火球音效
        AudioManager.PlayAudio(AudioName.PlayerFireBall);
        if (fireBallPrefab == null)
        {
            Debug.LogError("未设置火球预制件！");
            return;
        }
        // 在玩家位置上方1.7单位处生成火球
        Vector3 spawnPosition = player.transform.position + new Vector3(0f, 1.7f, 0f);
        GameObject fireBallObj = Instantiate(fireBallPrefab, spawnPosition, player.transform.rotation);
        FireBallController controller = fireBallObj.GetComponent<FireBallController>();

        if (controller != null)
        {
            controller.Initialize(speed, maxRange, damage, player.transform);
            StartCooldown();  // 技能释放成功后启动冷却
        }
        else
        {
            Debug.LogError("火球预制件上没有找到 FireBallController 组件");
            Destroy(fireBallObj);
        }
    }
}
