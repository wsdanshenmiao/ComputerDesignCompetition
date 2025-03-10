/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       玩家状态基类
****************************************************************************************/

using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    // 玩家的组件
    protected PlayerController playerController;
    protected PlayerInput playerInput;
    protected PlayerStateMachine playerStateMachine;
    protected Animator playerAnimator;

    // 两个动画之间的动画过渡时间
    [SerializeField, Range(0, 1)] protected float tranditionTime = .1f;
    [SerializeField] protected string stateName;
    // 状态动画的哈希值，用来快速索引
    protected int stateHash;

    /// <summary>
    /// 初始化玩家的状态
    /// </summary>
    /// <param name="playerController"></param> // 玩家的控制
    /// <param name="playerInput"></param>      // 玩家的输入
    /// <param name="stateMachine"></param>     // 玩家的状态机
    /// <param name="playerAnimator"></param>   // 玩家的动画
    public void InitPlayerState(
        PlayerController playerController,
        PlayerInput playerInput,
        PlayerStateMachine stateMachine,
        Animator playerAnimator)
    {
        this.playerController = playerController;
        this.playerInput = playerInput;
        this.playerStateMachine = stateMachine;
        this.playerAnimator = playerAnimator;
    }

    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public virtual void OnEnter()
    {
        playerAnimator.CrossFade(stateHash, tranditionTime);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void OnExit() { }

    protected float GetCurrSpeed()
    {
        PlayerSO playerPara = playerController.playerCharacter.playerPara;
        return playerController.isBigSize ? playerPara.walkSpeed : playerPara.runSpeed;
    }
}
