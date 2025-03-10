using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private RectTransform correctTrans;
    [SerializeField] private bool isFinished;
    private bool canDragging;
    
    RectTransform rectTransform;
    
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

    void DragAndDrop()
    {
        // 鼠标位置
        if (Input.GetMouseButtonDown(0)) {
            canDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canDragging = false;
            if (Mathf.Abs(rectTransform.position.x - correctTrans.position.x) < rectTransform.rect.width / 10 &&
                Mathf.Abs(rectTransform.position.y - correctTrans.position.y) < rectTransform.rect.height / 10) {
                rectTransform.position = correctTrans.position;
            }
            else {
                rectTransform.position = startPos;
            }
        }
        if (canDragging) {
            Vector2 pos = rectTransform.position;
            if (Mathf.Abs(Input.mousePosition.x - pos.x) < rectTransform.rect.width / 2 && 
                Mathf.Abs(Input.mousePosition.y - pos.y) < rectTransform.rect.height / 2) {
                rectTransform.position = Input.mousePosition;
            }
        }

        
    }
}
