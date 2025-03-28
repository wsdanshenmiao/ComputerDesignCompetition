using System;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [Header("事件广播")]
    [SerializeField] private VoidEventSO OnNewGameEvent;
    [SerializeField] private VoidEventSO AfterSceneLoadEvent;
    [SerializeField] private IntEvent OnDiologEndEvent;

    [SerializeField] private RectTransform menuCancas;


    protected bool isContinueGame = false;

    protected void OnEnable()
    {
        AfterSceneLoadEvent.OnEventRaised += AfterSceneLoad;
        OnDiologEndEvent.OnEventRaised += OnDiologEnd;
    }

    protected void Start()
    {
        menuCancas.gameObject.SetActive(true);
    }

    protected void OnDisable()
    {
        AfterSceneLoadEvent.OnEventRaised -= AfterSceneLoad;
        OnDiologEndEvent.OnEventRaised -= OnDiologEnd;
    }

    private void OnDiologEnd(int dialogIndex)
    {
        if (dialogIndex == 1) {
            menuCancas.gameObject.SetActive(true);
            GameManager.lockScene[0] = false;
            GameManager.currSceneIndex++;
            NewGame();
        }
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
        menuCancas.gameObject.SetActive(false);
        isContinueGame = false;
        
        OnNewGameEvent.RaiseEvent();
    }

    public void ReturnMenu()
    {
        menuCancas.gameObject.SetActive(true);
    }
}
