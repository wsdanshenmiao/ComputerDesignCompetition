using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;
    /// <summary>
    /// 淡入:逐渐变黑,调用该函数时启用事件
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }

    /// <summary>
    /// 淡出:逐渐变透明,调用该函数时启用事件
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }

    /// <summary>
    /// 执行方法
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="duration"></param>
    /// <param name="fadeIn">为true执行淡入,为false执行淡出</param>
    public void RaiseEvent(Color targetColor, float duration, bool fadeInOrOut)
    {
        OnEventRaised.Invoke(targetColor, duration, fadeInOrOut); // 启动时执行OnEventRaised的内部函数
    }
}
