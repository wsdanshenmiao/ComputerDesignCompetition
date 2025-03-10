using UnityEngine;
using System.Collections;

public class MagnetSkill : Skill
{
    [SerializeField] private float attractRadius = 3f; // 吸引半径
    [SerializeField] private float attractForce = 5f; // 吸引力度
    [SerializeField] private float attractDuration = 3f; // 吸引持续时间
    [SerializeField] private LayerMask ingredientLayer; // 在Inspector中设置食材所在的层
    private bool isAttracting = false;

    public override void UseSkill()
    {
        //TODO(audio) 增加磁力技能音效
        //磁力技能实现逻辑
        if (!isAttracting)
        {
            StartCoroutine(AttractCoroutine());
        }
        // 在技能成功释放后启动冷却
        StartCooldown();
    }

    private IEnumerator AttractCoroutine()
    {
        isAttracting = true;

        float timer = attractDuration;
        while (timer > 0)
        {
            Attract();
            timer -= Time.deltaTime;
            yield return null;
        }

        isAttracting = false;
    }

    private void Attract()
    {
        // 使用 LayerMask 只检测食材层级的碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, attractRadius, ingredientLayer);

        foreach (Collider2D collider in colliders)
        {
            Vector2 direction = (player.transform.position - collider.transform.position).normalized;

            collider.transform.position = Vector2.MoveTowards(
                collider.transform.position,
                player.transform.position,
                attractForce * Time.deltaTime
            );
        }
    }

    //用于在编辑器中可视化吸引范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (player != null)
        {
            Gizmos.DrawWireSphere(player.transform.position, attractRadius);
        }
    }
}
