/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.11
	Finish time:	
	Abstract:       玩家的待机状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Idle", fileName = "PlayerIdle")]
public class PlayerStateIdle : PlayerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        playerController.SetVelocityY(0);
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
        if (playerInput.isFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateFall));
            return;
        }
        if (playerInput.isMove)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateMove));
            return;
        }
        if (playerInput.isJump)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateJump));
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        playerController.SetVelocityX(0);
    }
}

