/****************************************************************************************
	Author:			Crusher
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       储存传送点应该具有的信息
****************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortailInfo : MonoBehaviour
{
    [HideInInspector] public GameSceneSO GameScene;
    [HideInInspector] public Vector3 Position;
    [HideInInspector] public Sprite Sprite;
    [HideInInspector] public string Description;
    [HideInInspector] public string PortalName;

    // Manager中Awak获取并赋值数据后,使用Start将内容写在文本上
    private void Start()
    {
        WriteInChild();
    }

    // 把字符串写进子物体的文本框中
    private void WriteInChild()
	{
		transform.GetComponentInChildren<TMP_Text>().text = PortalName;
	}
}
