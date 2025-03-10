using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("组件")]
    public CinemachineConfiner2D confinder;

    [Header("事件")]
    public VoidEventSO AfterSceneLoadEvent; // 场景加载完毕后要使用的事件对象

    protected override void Awake()
    {
        confinder = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        // 注册
        AfterSceneLoadEvent.OnEventRaised += GetNewBounds;
    }

    private void OnDisable()
    {
        AfterSceneLoadEvent.OnEventRaised -= GetNewBounds;
    }

    private void GetNewBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds"); // 找出带Bounds标签的对象

        confinder.m_BoundingShape2D = obj?.GetComponent<Collider2D>();
        confinder.InvalidateCache(); // 清理丢掉的缓存
    }
}
