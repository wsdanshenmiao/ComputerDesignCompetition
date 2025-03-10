/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.14
	Finish time:	2025.1.14
	Abstract:       玩家的死亡状态
****************************************************************************************/
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Death", fileName = "PlayerDeath")]
public class PlayerStateDeath : PlayerState
{
    public float fadeDuration = 1.0f;

    //[SerializeField] private FadeEventSO OnFadeEvent;

    public override void OnEnter()
    {
        base.OnEnter();

        playerController.playerCharacter.IsDeath = true;
        playerController.finishLoadScene = false;

        AudioManager.PlayAudio(AudioName.PlayerDeath);

        playerInput.DisableGamePlayInput();

        playerController.SetVelocityX(0);
        playerController.SetVelocityY(0);

        SaveManager.Instance.LoadLastScene();

        //OnFadeEvent.FadeIn(fadeDuration);
        //playerController.StartCoroutine(SwitchToIdle());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (playerController.finishLoadScene)
        {
            SaveManager.Instance.LoadGame();
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        // OnFadeEvent.FadeOut(fadeDuration);

        playerInput.EnableGamePlayInput();
        playerController.playerCharacter.IsDeath = false;
    }

    //private IEnumerator SwitchToIdle()
    //{
    //    yield return new WaitForSeconds(fadeDuration);
    //    playerStateMachine.SwitchState(typeof(PlayerStateIdle));
    //}

}

