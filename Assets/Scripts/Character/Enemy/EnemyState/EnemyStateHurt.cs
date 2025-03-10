/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的受伤状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/EnemyState/Hurt", fileName = "EnemyHurt")]
public class EnemyStateHurt : EnemyState
{
    // 受伤时的僵直时间
    private float currHurtStopTime = 0;

    public override void OnEnter()
    {
        base.OnEnter();
        currHurtStopTime = enemyController.enemyCharacter.charaPara.hurtStopTime;
    }

    public override void LogicUpdate()
    {
        currHurtStopTime -= Time.deltaTime;
        if (currHurtStopTime > 0) return;
        if (enemyController.enemyCharacter.IsDeath)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateDeath));
        }
        else
        {
            enemyStateMachine.SwitchState(typeof(EnemyStatePatrol));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        enemyController.enemyCharacter.IsHurt = false;
    }
}