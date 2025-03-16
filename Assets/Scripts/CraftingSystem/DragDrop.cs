using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    // 事件广播
    [SerializeField] private SlotChangeEvent SlotChangeEvent;

    private Item item;

    public Vector3 startPos;

    private bool canDragging;

    private RectTransform rectTransform;
    
    private TMP_Text  text;

    public float catchDis = 30;

    static private DragDrop catchDrag;  // 一次只能有一个移动, 没有拖动则为空
    static public CraftingSlot target;  // 一次只能锁定一个槽
    static private bool isFirst = true;
    static private bool firstIsDrag = false;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponentInChildren<TMP_Text>();
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

    public void SetTarget(CraftingSlot target)
    {
        DragDrop.target = target;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        if (text != null) {
            text.text = item?.amount.ToString();
        }
    }

    static public DragDrop GetCatchDrop()
    {
        return catchDrag;
    }

    private void DragAndDrop()
    {
        Vector2 pos = rectTransform.position;
        // 检测是否在图标上
        bool touchDrop = Mathf.Abs(Input.mousePosition.x - pos.x) < rectTransform.rect.width / 2 &&
                         Mathf.Abs(Input.mousePosition.y - pos.y) < rectTransform.rect.height / 2;

        if (catchDrag != null && catchDrag == this) {
            rectTransform.position = Input.mousePosition;
        }

        // 没碰到自己就走
        if (!(Input.GetMouseButtonDown(0) && touchDrop)) return;

        // 是否碰到合成槽
        if (target != null && // 没必要处理
            Mathf.Abs(rectTransform.position.x - target.rectTransform.position.x) < catchDis &&
            Mathf.Abs(rectTransform.position.y - target.rectTransform.position.y) < catchDis) {
            if (catchDrag == null) {
                catchDrag = this;
                target.item = null;
            }
            else if (target.item == null) {  // 直接放上去
                catchDrag.rectTransform.position = target.rectTransform.position;
                target.item = catchDrag.item;
                catchDrag = null;
            }
            else {
                if (isFirst) {
                    catchDrag.rectTransform.position = target.rectTransform.position;
                    target.item = catchDrag.item;
                    if (catchDrag != this) {
                        firstIsDrag = false;
                        catchDrag = this;
                    }
                    else {
                        firstIsDrag = true;
                    }
                    isFirst = false;
                }
                else {
                    if (firstIsDrag) {
                        catchDrag = this;
                    }
                    isFirst = true;
                }
            }
        }
        else {
            if (catchDrag == null) {
                catchDrag = this;
            }
            else {
                catchDrag.rectTransform.position = catchDrag.startPos;
                catchDrag = catchDrag == this ? null : this;
            }
        }
    }
}
