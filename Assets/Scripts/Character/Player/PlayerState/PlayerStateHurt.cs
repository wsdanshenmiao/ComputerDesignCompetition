/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	2025.1.14
	Abstract:       玩家的受伤状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Hurt", fileName = "PlayerHurt")]
public class PlayerStateHurt : PlayerState
{
    private float currHurtStopTime;

    public override void OnEnter()
    {
        base.OnEnter();
        playerController.playerCharacter.IsHurt = true;
        currHurtStopTime = playerController.playerCharacter.charaPara.hurtStopTime;
        playerInput.DisableGamePlayInput();

        AudioManager.PlayAudio(AudioName.PlayerHurt, true);
    }

    public override void LogicUpdate()
    {
        currHurtStopTime -= Time.deltaTime;
        if (currHurtStopTime > 0) return;

        if (playerController.playerCharacter.IsDeath)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateDeath));
        }
        else
        {
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
        }
    }

    public override void OnExit()
    {
        playerInput.EnableGamePlayInput();
        playerController.SetVelocityY(0);
        playerController.playerCharacter.IsHurt = false;
    }
}

