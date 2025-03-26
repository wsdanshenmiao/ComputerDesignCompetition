using UnityEngine;

public class Inventor : InteractableObject, I_Interactable
{
    public int DialogIndex;

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
        // 触发与导师的对话行为,打开DialogCanvans
        DialogManager.Instance.OpenDialog(DialogIndex);

        // 禁止时间
        Time.timeScale = 0;
    }
}