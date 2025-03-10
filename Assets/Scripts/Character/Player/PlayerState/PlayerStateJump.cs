/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.14
	Abstract:       玩家的跳跃状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Jump", fileName = "PlayerJump")]
public class PlayerStateJump : PlayerState
{
    private float jumpSpeed;
    private float originGravity;

    public override void OnEnter()
    {
        base.OnEnter();
        originGravity = playerController.GetGravityScale();

        PlayerSO playerPara = playerController.playerCharacter.playerPara;
        float gravity = Physics2D.gravity.y;

        float currentGravity = -1f * (2 * playerPara.jumpHeight) / Mathf.Pow(playerPara.jumpDuration, 2);
        float gravityScale = currentGravity / gravity;  // 计算出重力系数
        playerController.SetGravityScale(gravityScale);

        jumpSpeed = -1f * currentGravity * playerPara.jumpDuration; // 求出起跳速度
        playerController.SetVelocityY(jumpSpeed);

        AudioManager.PlayAudio(AudioName.PlayerJump);
    }

    public override void LogicUpdate()
    {
        if (playerController.playerCharacter.IsHurt)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateHurt));
            return;
        }
        if (playerInput.isFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateFall));
            return;
        }
        if (playerInput.canSprint)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateSprint));
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        playerController.Move(GetCurrSpeed());
    }

    public override void OnExit()
    {
        playerController.SetGravityScale(originGravity);
    }
}

