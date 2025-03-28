using UnityEngine;

public class Inventor : InteractableObject, I_Interactable
{
    public int DialogIndex;
    public int unlockSceneIndex;
    // 通关需要的合成物品
    [SerializeField] private ItemScriptableObject passItem;

    [SerializeField] private bool enablePass = false;

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
        if (!CraftingSystem.Instance.ContainsItem(passItem) && !enablePass) return;
        
        for (int i = 0; i < GameManager.lockScene.Length; ++i) {
            GameManager.lockScene[i] = true;
        }
        GameManager.lockScene[unlockSceneIndex] = false;
        GameManager.currSceneIndex++;
        DialogManager.Instance.OpenDialog(DialogIndex);

        // 禁止时间
        // Time.timeScale = 0;
    }
}