using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : InteractableObject, I_Interactable
{
    public DialogInfo DialogInfo;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    public void TriggerAction()
    {
        DialogManager.Instance.GetText(DialogInfo.Dialog); // 先将文本传递给Manager
        // 触发与导师的对话行为,打开DialogCanvans
        DialogManager.Instance.OpenDialog();

        // 禁止时间
        Time.timeScale = 0;

        // TODO: 停止除点击外的其他音效VFX
    }
}
