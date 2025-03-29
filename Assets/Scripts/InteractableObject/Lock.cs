using System;
using UnityEngine;
using System.Collections.Generic;

public class Lock : MonoBehaviour
{
    [SerializeField] private List<int> requiredKeyIDs; // 需要的钥匙ID列表

    private HashSet<int> collectedKeyIDs = new HashSet<int>();
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    
    private bool isUnlocked = false;
    public float unlockTransparency = 0.3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        // 订阅钥匙收集事件
        Key.OnKeyCollected += OnKeyCollected;
    }

    private void Start()
    {
        collider2D.isTrigger = false;
    }

    private void OnDisable()
    {
        // 取消订阅钥匙收集事件
        Key.OnKeyCollected -= OnKeyCollected;
    }

    private void OnKeyCollected(int collectedKeyID)
    {
        // 检查收集的钥匙ID是否在需求列表中
        if (!isUnlocked && requiredKeyIDs.Contains(collectedKeyID)) {
            collectedKeyIDs.Add(collectedKeyID);
            
            // 检查是否收集齐所有钥匙
            if (collectedKeyIDs.Count == requiredKeyIDs.Count) {
                isUnlocked = true;
                Color color = spriteRenderer.color;
                spriteRenderer.color = new Color(color.r, color.g, color.b, unlockTransparency);
                collider2D.isTrigger = true;
            }
        }
    }
}