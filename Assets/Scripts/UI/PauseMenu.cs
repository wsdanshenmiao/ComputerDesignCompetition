using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : Singleton<PauseMenu>
{
    [Header("主界面场景")]
    public GameSceneSO MainMenuScene;

    [Header("暂停面板的Transform")]
    public Transform PauseCanvas;
    private PlayerInputController controller;
    [SerializeField] private bool isOpen;

    [Header("事件监听")]
    public VoidEventSO AfterLoadSceneEvent;

    private bool retSavePoint = false;

    protected override void Awake()
    {
        base.Awake();

        controller = new PlayerInputController();
        controller.GamePlay.Pause.started += RasiePausePanel;
        isOpen = false;
    }


    private void OnEnable()
    {
        controller?.Enable();
        AfterLoadSceneEvent.OnEventRaised += AfterLoadScene;
    }

    private void OnDisable()
    {
        controller?.Disable();
        AfterLoadSceneEvent.OnEventRaised -= AfterLoadScene;
    }

    private void AfterLoadScene()
    {
        if (retSavePoint)
        {
            SaveManager.Instance.LoadGame();
            retSavePoint = false;
        }
    }

    private void RasiePausePanel(InputAction.CallbackContext context)
    {
        if (PauseCanvas.parent.gameObject.activeSelf == false) // 如果父物体未开启,不能使用此功能
        {
            return;
        }

        if (isOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    private void ClosePanel()
    {
        PauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1; // 运行
        AudioManager.OpenMasterAudio();
        isOpen = false;
    }

    private void OpenPanel()
    {
        PauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0; // 暂停
        AudioManager.CloseMasterAudio();
        isOpen = true;
    }

    public void ExitGame()
    {
        // 关闭当前的菜单
        ClosePanel();

        Vector3 tmpPos = Vector3.zero; // 把Vector3.Zero的改了
        SceneManager.Instance.PortalToNew(MainMenuScene, tmpPos);
        MenuManager.Instance.ReturnMenu();
    }

    public void ReloadGame()
    {
        // 关闭当前菜单
        ClosePanel();

        if (SaveManager.Instance.LoadLastScene())
        {
            retSavePoint = true;
        }
    }

    public void ContinueGame()
    {
        // 关闭当前菜单
        ClosePanel();
    }
}
