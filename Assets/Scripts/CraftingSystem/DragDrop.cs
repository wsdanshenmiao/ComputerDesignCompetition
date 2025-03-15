using TMPro;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    // 事件广播
    [SerializeField] private SlotChangeEvent SlotChangeEvent;

    private Item item;

    private Vector3 startPos;

    private CraftingSlot target;

    private bool canDragging;

    private RectTransform rectTransform;
    
    private TMP_Text  text;

    public float catchDis = 30;

    // 一次只能移动一个， 防止重复移动, 为null时则没有拖动物品
    static private DragDrop catchDrag;

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
        this.target = target;
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
        
        if (Input.GetMouseButtonDown(0) && touchDrop)
        {
            // 检测先前是否拿起物品,拿起物品则可以放下
            if (canDragging)    // 拿起来了要放下
            {
                // 检测点击时是否在合成槽中
                if (Mathf.Abs(rectTransform.position.x - target.rectTransform.position.x) < catchDis &&
                    Mathf.Abs(rectTransform.position.y - target.rectTransform.position.y) < catchDis) {
                    rectTransform.position = target.rectTransform.position;
                    target.item = item;
                }
                else {
                    rectTransform.position = startPos;
                    target.item = null;
                }

                catchDrag = null;
            }
            else {  // 要拿起来
                catchDrag = this;
                if (target != null) {
                    target.item = null;
                }
            }
            // 切换状态
            canDragging = !canDragging;
        }
        if (canDragging)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
