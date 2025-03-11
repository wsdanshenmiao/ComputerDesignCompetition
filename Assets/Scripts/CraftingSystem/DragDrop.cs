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

    public float catchDis = 1;

    // 一次只能移动一个， 防止重复移动, 为null时则没有拖动物品
    static private DragDrop catchDrag;

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

    public void SetTarget(CraftingSlot target)
    {
        this.target = target;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    static public DragDrop GetCatchDrop()
    {
        return catchDrag;
    }

    private void DragAndDrop()
    {
        // 鼠标位置
        if (Input.GetMouseButtonDown(0))
        {
            canDragging = true;
            if (target != null)
            {
                target.item = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canDragging = false;

            if (target != null &&
                Mathf.Abs(rectTransform.position.x - target.rectTransform.position.x) < catchDis &&
                Mathf.Abs(rectTransform.position.y - target.rectTransform.position.y) < catchDis)
            {
                rectTransform.position = target.rectTransform.position;
                target.item = item;
            }
            else
            {
                rectTransform.position = startPos;
            }
            catchDrag = null;
        }
        if (canDragging)
        {
            Vector2 pos = rectTransform.position;
            if (Mathf.Abs(Input.mousePosition.x - pos.x) < rectTransform.rect.width / 2 &&
                Mathf.Abs(Input.mousePosition.y - pos.y) < rectTransform.rect.height / 2 &&
                catchDrag == null || catchDrag == this)
            {
                catchDrag = this;
                rectTransform.position = Input.mousePosition;
            }
        }


    }
}
