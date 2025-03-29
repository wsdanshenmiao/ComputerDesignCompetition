using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class Key : MonoBehaviour
{
    [SerializeField] private int keyID;  // 用于匹配对应的锁
    public int KeyID => keyID;  // 只读属性

    public static event System.Action<int> OnKeyCollected;  // 钥匙被收集时触发的事件

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            UnlockLock();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            UnlockLock();
        }
    }

    public void UnlockLock()
    {
        OnKeyCollected?.Invoke(KeyID);
        
        // 解锁后销毁
        gameObject.SetActive(false);
    }
}
