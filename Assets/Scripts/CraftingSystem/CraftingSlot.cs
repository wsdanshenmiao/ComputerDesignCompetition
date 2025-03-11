using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 制作槽
/// </summary>
public class CraftingSlot : MonoBehaviour
{
    [HideInInspector] public int2 index;
    [HideInInspector] public Item item;

    // 合成事件广播
    [SerializeField] private VoidEventSO CompoundEvent;

    public bool isOutputSlot = false;

    public RectTransform rectTransform;

    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
    }

    private void Update()
    {
        if (!isOutputSlot) return;

        Vector2 pos = rectTransform.position;
        if (Input.GetMouseButtonDown(0) &&
            Mathf.Abs(Input.mousePosition.x - pos.x) < rectTransform.rect.width / 2 &&
            Mathf.Abs(Input.mousePosition.y - pos.y) < rectTransform.rect.height / 2)
        {
            if (item != null)
            {
                CraftingSystem.Instance.AddItem(item);
                item = null;
                image.sprite = null;
                CompoundEvent?.OnEventRaised();
            }
        }
    }


    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}