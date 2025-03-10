using UnityEngine;

public class FireBallController : MonoBehaviour
{
    private float speed;        // 飞行速度
    private float maxRange;     // 最大射程
    private int damage;         // 伤害值

    private Vector3 startPosition;    // 初始位置
    private Rigidbody2D rb;            // 刚体组件


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 新增初始化方法
    public void Initialize(float speed, float maxRange, int damage, Transform player)
    {
        this.speed = speed;
        this.maxRange = maxRange;
        this.damage = damage;
        
        startPosition = player.position;
        rb.velocity = new Vector2(speed * Mathf.Sign(player.transform.localScale.x), 0);
    }

    private void Update()
    {
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Vine>() != null)
        {
            Debug.Log("检测到藤蔓");
            Vine _vine = collision.GetComponent<Vine>();
            _vine.DestroyVine();
            Destroy(gameObject);

        }


    }
}
