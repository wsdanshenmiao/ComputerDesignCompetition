/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	
	Abstract:       音频的名字
****************************************************************************************/

using UnityEngine.Rendering.LookDev;

public class AudioName
{
    /// <summary>
    /// 玩家的音效
    /// </summary>
    public static string PlayerJump { get => "PlayerJump"; }
    public static string PlayerWalk { get => "PlayerWalk"; }
    public static string PlayerLand { get => "PlayerLand"; }
    public static string PlayerHurt { get => "PlayerHurt"; }
    public static string PlayerDeath { get => "PlayerDeath"; }
    public static string PlayerSprint { get => "PlayerSprint"; }
    public static string PlayerBigSprint { get => "PlayerBigSprint"; }
    public static string PlayerCloned { get => "PlayerCloned"; }
    public static string PlayerChangeSize { get => "PlayerChangeSize"; }
    public static string PlayerPick { get => "PlayerPick"; }
    public static string PlayerFireBall { get => "PlayerFireBall"; }
    public static string PlayerMagnet { get => "PlayerMagnet"; }

    /// <summary>
    /// 敌人音效s
    /// </summary>
    public static string EnemyChase { get => "EnemyChase"; }
    public static string EnemyHurt { get => "EnemyHurt"; }
    public static string EnemyDeath { get => "EnemyDeath"; }

    /// <summary>
    /// 场景音效
    /// </summary>
    public static string BGM1 { get => "BGM1"; }
    public static string BGM2 { get => "BGM2"; }
    public static string VineDestory { get => "VineDestory"; }
    public static string PasswordTileDisappeared { get => "PasswordTileDisappeared"; }
    public static string PushBox { get => "PushBox"; }
    public static string BoxLand { get => "BoxLand"; }

    /// <summary>
    /// UI音效
    /// </summary>
    public static string ClickButton { get => "ClickButton"; }
    public static string ToggleButton { get => "ToggleButton"; }
}
