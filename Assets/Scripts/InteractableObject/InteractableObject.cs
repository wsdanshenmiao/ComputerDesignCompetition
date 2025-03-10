using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected virtual void Start()
    {
        gameObject.tag = "Interactable";
        
        // 获取或添加 BoxCollider2D 组件
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            // 设置 BoxCollider2D 的属性
            boxCollider.size = new Vector2(2f, 4f);
            boxCollider.offset = Vector2.zero;  // 等同于 new Vector2(0f, 0f)
            boxCollider.isTrigger = true;

        }
    }
    protected virtual void Reset()
    {
        gameObject.tag = "Interactable";
        
        // 检查是否已经存在 BoxCollider2D，如果不存在则添加
        if (GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
}
