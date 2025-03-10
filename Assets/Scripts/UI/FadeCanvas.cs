using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    [Header("事件监听")]
    public FadeEventSO fadeEvent;

    public Image fadeImage;

    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent; // 注册
    }
    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent; // 注销
    }

    /// <summary>
    /// 触发淡入淡出的事件
    /// </summary>
    /// <param name="targetColor">淡入或者淡出的颜色</param>
    /// <param name="duration">淡入或者淡出的持续时间</param>
    /// <param name="fadeInOrOut">选择淡入还是淡出</param>
    private void OnFadeEvent(Color targetColor, float duration, bool fadeInOrOut) // 使用普通的事件方法
    {
        fadeImage.DOBlendableColor(targetColor, duration);
    }
}
