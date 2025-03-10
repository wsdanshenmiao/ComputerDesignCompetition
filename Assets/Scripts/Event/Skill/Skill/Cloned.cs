using UnityEngine;

public class Cloned : Skill
{
    [Header("分身技能设置")]
    public float duration = 5f; // 持续时间

    private GameObject currentClone; // 当前的分身实例
    private bool isInvisible = false; // 是否处于隐身状态
    private float currentDuration; // 当前持续时间
    [SerializeField] private SpriteRenderer[] playerRenderers; // 改为数组存储所有SpriteRenderer
    //private Collider2D playerCollider; // 玩家碰撞体
    public GameObject clonePrefab; // 分身预制体
    private PlayerCharacter playerCharacter;

    [SerializeField] Transform characterTransform;

    protected override void Start()
    {
        // 调用基类的Start方法来确保player被正确初始化
        base.Start();
        playerCharacter = player.gameObject.GetComponent<PlayerCharacter>();
        // 直接在场景中查找PlayerCharacter
        if (playerCharacter == null)
        {
            Debug.LogError("无法找到PlayerCharacter组件");
        }
        else
        {
            playerCharacter = FindObjectOfType<PlayerCharacter>();
        }

        if (characterTransform != null)
        {
            // 从Character对象获取所有SpriteRenderer组件
            playerRenderers = characterTransform.GetComponentsInChildren<SpriteRenderer>();
            //if (playerRenderers.Length == 0)
            //{
            //    Debug.LogError("未能在Player中找到任何SpriteRenderer组件！");
            //}
        }
        else
        {
            Debug.LogError("未能找到Player对象！");
        }

        //playerCollider = player.GetComponent<Collider2D>();
    }

    // 新增：设置所有精灵渲染器的透明度
    private void SetPlayerTransparency(float alpha)
    {
        foreach (var renderer in playerRenderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }

    protected override void Update()
    {
        // 更新持续时间
        if (isInvisible)
        {
            currentDuration -= Time.deltaTime;
            if (currentDuration <= 0)
            {
                DeactivateCloneSkill();
            }
        }
    }

    public override void UseSkill()
    {
        Debug.Log("成功释放分身");
        ActivateCloneSkill();
        //TODO(audio) 增加分身音效
    }

    private void ActivateCloneSkill()
    {
        if (isInvisible) return;

        currentClone = Instantiate(clonePrefab, player.transform.position, player.transform.rotation);
        // 初始化分身的外观

        // 设置玩家隐身且不可被追踪
        isInvisible = true;
        SetPlayerTransparency(0.2f);
        playerCharacter.SetTargetable(false);

        currentDuration = duration;
    }

    private void DeactivateCloneSkill()
    {
        if (currentClone != null)
        {
            currentClone.GetComponent<ClonedPrefab>().Delete();
        }

        // 恢复玩家显示和可追踪状态
        isInvisible = false;
        SetPlayerTransparency(1f); // 恢复完全不透明
        playerCharacter.SetTargetable(true);
    }

    //TODO:要把这个碰撞检测放在玩家上
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvisible && other.CompareTag("Enemy"))
        {
            DeactivateCloneSkill();
        }
    }
}
