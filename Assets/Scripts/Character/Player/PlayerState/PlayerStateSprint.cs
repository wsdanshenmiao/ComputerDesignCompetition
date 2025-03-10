/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       玩家的冲刺状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Sprint", fileName = "PlayerSprint")]
public class PlayerStateSprint : PlayerState
{
    private float originGravity;
    private float currTime;

    public override void OnEnter()
    {
        base.OnEnter();

        PlayerSO playerPara = playerController.playerCharacter.playerPara;

        originGravity = playerController.GetGravityScale();
        currTime = playerPara.sprintDuration;

        // 冲刺时不下坠
        playerController.SetVelocityY(0);
        playerController.SetGravityScale(0);
    }

    public override void LogicUpdate()
    {
        currTime -= Time.deltaTime;
        if (currTime > 0) return;

        if (playerController.playerCharacter.IsHurt)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateHurt));
            return;
        }
        if (playerInput.isFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateFall));
        }
        else
        {
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
        }
    }

    public override void PhysicsUpdate()
    {
        PlayerSO playerPara = playerController.playerCharacter.playerPara;
        Vector3 scale = playerController.transform.localScale;
        float sign = scale.x / (scale.x == 0 ? 1 : Mathf.Abs(scale.x));
        playerController.SetVelocityX(playerPara.sprintSpeed * sign);
    }

    public override void OnExit()
    {
        playerController.SetGravityScale(originGravity);
        playerController.SetVelocityX(0);
        PlayerCharacter playerCharacter = playerController.playerCharacter;
        playerCharacter.currSprintCD = playerCharacter.playerPara.sprintCD;
    }
}
