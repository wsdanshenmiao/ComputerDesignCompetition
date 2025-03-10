/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.14
	Abstract:       玩家的下坠状态
****************************************************************************************/

using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Fall", fileName = "PlayerFall")]
public class PlayerStateFall : PlayerState
{
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
    }

    public override void LogicUpdate()
    {
        if (playerController.playerCharacter.IsHurt)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateHurt));
            return;
        }
        if (playerInput.canSprint)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateSprint));
            return;
        }
        if (playerController.isGround)
        {
            AudioManager.PlayAudio(AudioName.PlayerLand);
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
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

