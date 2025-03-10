/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	2025.1.12
	Abstract:       管理角色的伤害
****************************************************************************************/
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("攻击的各项属性")]
    private int damage;          // 攻击力
    private float attackRate;    // 攻击频率
    private float knockFactor;   // 击退
    public LayerMask attackLayer;

    private float currAttackRate;

    private void Start()
    {
        currAttackRate = attackRate;
    }

    private void Update()
    {
        currAttackRate -= Time.deltaTime;
    }

    public void SetAttackData(int damage, float attackRate, float knockFactor)
    {
        this.damage = damage;
        this.attackRate = attackRate;
        this.knockFactor = knockFactor;
    }

    protected void OnCollisionStay2D(Collision2D coll)
    {
        if (IsInLayerMask(coll.gameObject, attackLayer) && currAttackRate < 0)
        {
            coll.collider.GetComponent<Character>()?.TakeDamege(this);
            currAttackRate = attackRate;
        }
    }

    protected void OnTriggerStay2D(Collider2D coll)
    {
        if (IsInLayerMask(coll.gameObject, attackLayer) && currAttackRate < 0)
        {
            coll.GetComponent<Character>()?.TakeDamege(this);
            currAttackRate = attackRate;
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetKnockFactor()
    {
        return knockFactor;
    }

    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // 根据Layer数值进行移位获得用于运算的Mask值
        int objLayerMask = 1 << obj.layer;
        return (layerMask.value & objLayerMask) > 0;
    }
}
