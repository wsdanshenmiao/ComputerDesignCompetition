/****************************************************************************************
	Filename:		IState.cs
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       状态接口
****************************************************************************************/

public interface IState
{
    /// <summary>
    /// 进入时调用,一般是OnEnable函数中调用
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// 逻辑判断,放在Update函数中调用
    /// </summary>
    public abstract void LogicUpdate();

    /// <summary>
    /// 物理判断,放在FixedUpdate函数中调用
    /// </summary>
    public abstract void PhysicsUpdate();

    /// <summary>
    /// 离开时调用,一般是OnDisable函数中调用
    /// </summary>
    public abstract void OnExit();
}
