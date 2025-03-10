/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       通过InputSystem管理玩家的输入
****************************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputController inputController;
    private PlayerController playerController;
    private Vector2 inputAxes => inputController.GamePlay.Move.ReadValue<Vector2>();

    public bool isJump => canJump && playerController.isGround;
    public bool canJump => inputController.GamePlay.Jump.WasPressedThisFrame();
    public bool isFalling => (playerController.GetVelocityY() <= 0f) && (!playerController.isGround);
    public bool isMove => inputAxes.x != 0;
    public bool canSprint =>
        playerController.playerCharacter.currSprintCD < 0 &&
        inputController.GamePlay.Sprint.WasPressedThisFrame();
    public bool isClick => inputController.GamePlay.MouseDown.WasPressedThisFrame();

    public float moveSpeed => playerController.GetVelocityX();
    public float inputAxesX => inputAxes.x;


    [SerializeField] private bool showEnableGamePlay;

    private void Awake()
    {
        inputController = new PlayerInputController();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (showEnableGamePlay)
        {
            Debug.Log(inputController.GamePlay.enabled);
        }
        if (isClick)
        {
            AudioManager.PlayAudio(AudioName.ClickButton, true);
        }
    }

    /// <summary>
    /// 开启GamePlay下的输入控制
    /// </summary>
    public void EnableGamePlayInput()
    {
        inputController.GamePlay.Enable();
    }

    /// <summary>
    /// 关闭GamePlay下的输入控制
    /// </summary>
    public void DisableGamePlayInput()
    {
        inputController.GamePlay.Disable();
    }
    /// <summary>
    /// 开启UI下的输入控制
    /// </summary>
    public void EnabelUIInput()
    {
        inputController.UI.Enable();
    }

    /// <summary>
    /// 关闭UI下的输入控制
    /// </summary>
    public void DisableUIInput()
    {
        inputController.UI.Disable();
    }
}
