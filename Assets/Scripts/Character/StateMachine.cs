using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
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
