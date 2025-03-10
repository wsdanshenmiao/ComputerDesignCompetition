/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的死亡状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/EnemyState/Death", fileName = "EnemyDeath")]
public class EnemyStateDeath : EnemyState
{
    public override void OnEnter()
    {
        base.OnEnter();
        AudioManager.PlayAudio(AudioName.EnemyDeath);
        enemyController.SetVelocityX(0);
        enemyController.SetVelocityY(0);
        enemyController.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!enemyController.enemyCharacter.IsDeath)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateIdle));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        enemyController.gameObject.SetActive(true);
        enemyController.enemyCharacter.IsDeath = false;
    }
}