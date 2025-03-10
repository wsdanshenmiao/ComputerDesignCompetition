/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的巡逻状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/EnemyState/Patrol", fileName = "EnemyPatrol")]
public class EnemyStatePatrol : EnemyState
{
    public override void LogicUpdate()
    {
        float sign = enemyController.transform.localScale.x;

        if (enemyController.enemyCharacter.IsDeath)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateDeath));
        }
        if (enemyController.enemyCharacter.IsHurt)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateHurt));
        }
        if (enemyController.FindPlayer() != Vector3.zero)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateChase));
        }
        if (sign > 0 && enemyController.touchRight ||
            sign < 0 && enemyController.touchLeft ||
           !enemyController.isGround)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateIdle));
        }
    }

    public override void PhysicsUpdate()
    {
        enemyController.Walk();
    }
}