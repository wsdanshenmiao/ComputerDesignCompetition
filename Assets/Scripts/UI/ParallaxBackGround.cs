using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private Transform CameraTransform;
    private Vector3 CameraPosLastFrame;
    private Sprite sprite;
    private float textureSizeX; // 获取图片横轴的大小

    [Header("运动系数")]
    public Vector2 moveCoefficient; // 移动时的系数

    [Header("是否允许滚动")]
    public bool canInfiniteRollX; // 是否允许X轴无限滚动
    public bool canInfiniteRollY; // 是否允许Y轴无限滚动

    private void Awake()
    {
        CameraTransform = Camera.main.transform; // 获取主相机的位置
        CameraPosLastFrame = CameraTransform.position; // 一开始获取此时相机的位置
        sprite = GetComponent<SpriteRenderer>().sprite;
        textureSizeX = (sprite.texture.width / sprite.pixelsPerUnit) * transform.localScale.x; // 获取图片长度
    }

    private void Update()
    {
        Vector2 deltaMovement = new Vector2(CameraTransform.position.x - CameraPosLastFrame.x, CameraTransform.position.y - CameraPosLastFrame.y);
        transform.position = transform.position + new Vector3(deltaMovement.x * moveCoefficient.x, deltaMovement.y * moveCoefficient.y, 0); // 
        CameraPosLastFrame = CameraTransform.position; // 获取此时的位置

        if (Mathf.Abs(CameraTransform.position.x - transform.position.x) >= textureSizeX)
        {
            transform.position = new Vector3(CameraTransform.position.x, transform.position.y, transform.position.z); // 重新回到一开始的位置
        }
    }
}