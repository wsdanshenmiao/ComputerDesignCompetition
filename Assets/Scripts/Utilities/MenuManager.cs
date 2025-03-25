/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       主菜单管理
****************************************************************************************/
using System;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [Header("事件广播")]
    [SerializeField] private VoidEventSO OnNewGameEvent;
    [SerializeField] private VoidEventSO AfterSceneLoadEvent;

    [SerializeField] private RectTransform menuCancas;


    protected bool isContinueGame = false;

    protected void OnEnable()
    {
        AfterSceneLoadEvent.OnEventRaised += AfterSceneLoad;
    }

    protected void Start()
    {
        menuCancas.gameObject.SetActive(true);
    }

    protected void OnDisable()
    {
        AfterSceneLoadEvent.OnEventRaised -= AfterSceneLoad;
    }

    private void AfterSceneLoad()
    {
        if (isContinueGame)
        {
            SaveManager.Instance.LoadGame();
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ContinueGame()
    {
        menuCancas.gameObject.SetActive(false);

        Debug.Log("ContinueGame");
        if (SaveManager.Instance.LoadLastScene())
        {
            isContinueGame = true;
        }
        else
        {
            SaveManager.Instance.NewGame();
            OnNewGameEvent.RaiseEvent();
            isContinueGame = false;
        }
    }

    public void NewGame()
    {
        Debug.Log("NewGame");
        menuCancas.gameObject.SetActive(false);
        isContinueGame = false;
        
        OnNewGameEvent.RaiseEvent();
    }

    public void ReturnMenu()
    {
        menuCancas.gameObject.SetActive(true);
    }
}
