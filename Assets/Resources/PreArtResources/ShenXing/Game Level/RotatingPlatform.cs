using System;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public Transform center;          // 旋转中心
    public float radius = 2f;         // 旋转半径
    public float angularSpeed = 30f;  // 旋转速度（度/秒）
    public float initialAngleOffset;  // 初始角度偏移（度）

    private Rigidbody2D rb;
    private float currentAngle;
    
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAngle = initialAngleOffset;
    }

    void FixedUpdate()
    {
        // 更新角度
        currentAngle += angularSpeed * Time.fixedDeltaTime;
        float angleRad = currentAngle * Mathf.Deg2Rad;

        Vector3 prepos = transform.position;
        
        // 计算新位置
        Vector2 newPos = new Vector2(
            Mathf.Cos(angleRad) * radius,
            Mathf.Sin(angleRad) * radius
        );


        // 移动平台
        rb.MovePosition(center.position + new Vector3(newPos.x, newPos.y, 0));
        transform.rotation = Quaternion.identity; // 保持水平
        
        if (player != null)
        {
            player.position += transform.position - prepos;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<Transform>();
            // other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            Debug.Log("-------2--------");
            // other.gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<Transform>();
            player = other.gameObject.transform;
            Debug.Log("-------1--------");
        }
    }
}