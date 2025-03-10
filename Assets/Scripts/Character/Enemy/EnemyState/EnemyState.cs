/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人状态基类
****************************************************************************************/

using UnityEngine;

public class EnemyState : ScriptableObject, IState
{
    // 玩家的组件
    protected EnemyController enemyController;
    protected EnemyStateMachine enemyStateMachine;
    protected Animator enemyAnimator;

    // 两个动画之间的动画过渡时间
    [SerializeField, Range(0, 1)] protected float tranditionTime = .1f;
    [SerializeField] protected string stateName;
    // 状态动画的哈希值，用来快速索引
    protected int stateHash;

    /// <summary>
    /// 初始化玩家的状态
    /// </summary>
    /// <param name="enemyController"></param>  // 玩家的控制
    /// <param name="stateMachine"></param>     // 玩家的状态机
    /// <param name="enemyAnimator"></param>   // 玩家的动画
    public void InitPlayerState(
        EnemyController enemyController,
        EnemyStateMachine stateMachine,
        Animator enemyAnimator)
    {
        this.enemyController = enemyController;
        this.enemyStateMachine = stateMachine;
        this.enemyAnimator = enemyAnimator;
    }

    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public virtual void OnEnter()
    {
        enemyAnimator.CrossFade(stateHash, tranditionTime);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void OnExit() { }
}
