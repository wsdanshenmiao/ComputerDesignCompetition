using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    [SerializeField] private bool isFinished;
    private bool canDragging;
    
    private RectTransform rectTransform;

    public float catchDis = 1;
    
    // 一次只能移动一个， 防止重复移动, 为null时则没有拖动物品
    static public DragDrop catchDrag;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        canDragging = false;
        startPos = rectTransform.position;
    }

    void Update()
    {
        DragAndDrop();
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
    
    private void DragAndDrop()
    {
        // 鼠标位置
        if (Input.GetMouseButtonDown(0)) {
            canDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canDragging = false;
            if (Mathf.Abs(rectTransform.position.x - targetPos.x) < catchDis&&
                Mathf.Abs(rectTransform.position.y - targetPos.y) < catchDis) {
                rectTransform.position = targetPos;
            }
            else {
                rectTransform.position = startPos;
            }
            catchDrag = null;
        }
        if (canDragging) {
            Vector2 pos = rectTransform.position;
            if (Mathf.Abs(Input.mousePosition.x - pos.x) < rectTransform.rect.width / 2 && 
                Mathf.Abs(Input.mousePosition.y - pos.y) < rectTransform.rect.height / 2 &&
                catchDrag == null || catchDrag == this) {
                catchDrag = this;
                rectTransform.position = Input.mousePosition;
            }
        }

        
    }
}
