using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocityYToZero : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private PhysicsCheck _physicsCheck;
    
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (!_physicsCheck.isGround)
        //     return;
        
        Debug.Log("isGround: " + _physicsCheck.isGround);
        
        // 双重校验：既要检测到"Ground"，也要确保触发源是_circleCollider2D
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && IsTriggeredByBoxCollider(other))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        }
    }

    /// <summary>
    /// 验证触发源是否来自指定的圆形碰撞器
    /// </summary>
    private bool IsTriggeredByBoxCollider(Collider2D other)
    {
        // 获取当前对象与对方碰撞器接触的所有碰撞器
        var contacts = new List<Collider2D>();
        other.GetContacts(contacts);

        // 检查接触列表中是否包含我们的圆形碰撞器
        return contacts.Contains(_boxCollider2D);
    }
}
