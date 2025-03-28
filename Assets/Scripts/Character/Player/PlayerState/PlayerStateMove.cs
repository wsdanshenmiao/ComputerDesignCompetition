/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	2025.1.14
	Abstract:       玩家的移动状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Move", fileName = "PlayerMove")]
public class PlayerStateMove : PlayerState
{
    public override void OnEnter()
    {
        string currStateName = playerController.isBigSize ? "Walk" : "Run";
        playerAnimator.CrossFade(stateName + currStateName, tranditionTime);
        AudioManager.PlayAudio(AudioName.PlayerWalk);
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
        if (!playerInput.isMove)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            return;
        }
        if (playerInput.isJump)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateJump));
            return;
        }
        if (playerInput.isFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateCoyoteTime));
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        if (!playerController.isGround) {
            playerController.SetVelocityY(0);
        }
        playerController.Move(GetCurrSpeed());
    }

    public override void OnExit()
    {
        AudioManager.StopAudio(AudioName.PlayerWalk);
    }
}

