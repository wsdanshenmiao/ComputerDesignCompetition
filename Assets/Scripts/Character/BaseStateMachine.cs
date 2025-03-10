/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.11
	Abstract:       基础状态机，用来管理当前的状态
****************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    public bool showCurrState = false;

    // 当前状态
    protected IState currState;
    // 拥有的状态
    protected Dictionary<System.Type, IState> stateTable;

    virtual protected void Awake()
    {
        stateTable = new Dictionary<System.Type, IState>();
    }

    /// <summary>
    /// 进行逻辑更新
    /// </summary>
    void Update()
    {
        if (showCurrState)
        {
            Debug.Log(currState.GetType().Name);
        }
        currState.LogicUpdate();
    }

    /// <summary>
    /// 进行物理相关的更新
    /// </summary>
    void FixedUpdate()
    {
        currState.PhysicsUpdate();
    }

    /// <summary>
    /// 切换到下一个状态
    /// </summary>
    /// <param name="nextState"></param>    下一个状态
    protected void SwitchOn(IState nextState)
    {
        currState = nextState;
        currState.OnEnter();
    }

    /// <summary>
    /// 退出当前状态，并切换到下一个状态
    /// </summary>
    /// <param name="nextStateType"></param>    下一个状态的类型
    public void SwitchState(System.Type nextStateType)
    {
        IState nextState;
        if (stateTable.TryGetValue(nextStateType, out nextState))
        {
            currState.OnExit();
            SwitchOn(nextState);
        }
        else
        {
            Debug.LogError("The state machine does not have a current state");
        }
    }

}
