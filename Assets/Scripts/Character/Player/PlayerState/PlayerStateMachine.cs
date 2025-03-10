/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       玩家的状态机，用来管理玩家的状态
****************************************************************************************/

using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    [SerializeField] private PlayerState[] playerStates;

    protected override void Awake()
    {
        base.Awake();
        PlayerController playerController = GetComponent<PlayerController>();
        PlayerInput playerInput = GetComponent<PlayerInput>();
        // Animator animator = GetComponent<Animator>();
        Animator animator = GetComponentInChildren<Animator>(true);

        // 初始化状态字典
        foreach (var state in playerStates)
        {
            state.InitPlayerState(playerController, playerInput, this, animator);
            stateTable.Add(state.GetType(), state);
        }
    }

    private void Start()
    {
        IState idleState;
        if (stateTable.TryGetValue(typeof(PlayerStateIdle), out idleState))
        {
            SwitchOn(idleState);
        }
        else
        {
            Debug.LogError("Player StateMachine Have No Idle State");
        }

    }

    //判定当前状态
    public bool IsCurrentState<T>() where T : PlayerState
    {
        return currState.GetType() == typeof(T);
    }

    //获取状态
    public T GetState<T>() where T : PlayerState
    {
        return stateTable[typeof(T)] as T;
    }
}
