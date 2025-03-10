/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       玩家的土狼时间状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/CoyoteTime", fileName = "PlayerCoyoteTime")]
public class PlayerStateCoyoteTime : PlayerState
{
    private float originGravity;
    private float currTime;

    public override void OnEnter()
    {
        string currStateName = playerController.isBigSize ? "Walk" : "Run";
        playerAnimator.CrossFade(stateName + currStateName, tranditionTime);

        PlayerSO playerPara = playerController.playerCharacter.playerPara;

        originGravity = playerController.GetGravityScale();
        currTime = playerPara.coyoteDuration;

        // 土狼时间不下坠
        playerController.SetVelocityY(0);
        playerController.SetGravityScale(0);
    }

    public override void LogicUpdate()
    {
        currTime -= Time.deltaTime;

        if (playerController.playerCharacter.IsHurt)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateHurt));
        }
        if (playerInput.canJump)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateJump));
        }
        if (currTime < 0 || !playerInput.isMove)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateFall));
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
