/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的属性管理
****************************************************************************************/
using UnityEngine;

public class EnemyCharacter : Character
{
    public EnemySO templateEnemyPara;
    [HideInInspector] public EnemySO enemyPara;

    [SerializeField] private bool canChase = true;

    public bool CanChase => canChase;

    protected override void Awake()
    {
        base.Awake();
        Transform[] parents = GetComponentsInParent<Transform>();
        foreach (Transform parent in parents)
        {
            if (parent != null && parent.gameObject.name != "Ghost")
            {
                string name = parent.gameObject.name;
                dataDef.dataID += name;
                break;
            }
        }
    }

    protected override void ResetData()
    {
        base.ResetData();
        enemyPara = Instantiate(templateEnemyPara);
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制索敌范围虚球
        Gizmos.DrawWireSphere(transform.position, templateEnemyPara.checkRange);
    }
}
