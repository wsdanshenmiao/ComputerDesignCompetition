/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的待机状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/EnemyState/Idle", fileName = "EnemyIdle")]
public class EnemyStateIdle : EnemyState
{
    private float currIdleTime = 0;

    public override void OnEnter()
    {
        base.OnEnter();
        currIdleTime = enemyController.enemyCharacter.enemyPara.idleTime;
    }

    public override void LogicUpdate()
    {
        currIdleTime -= Time.deltaTime;
        if (enemyController.enemyCharacter.IsDeath)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateDeath));
        }
        if (enemyController.enemyCharacter.IsHurt)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateHurt));
        }
        if (enemyController.FindPlayer() != Vector3.zero &&
            enemyController.isGround)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateChase));
        }
        else if (currIdleTime < 0)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStatePatrol));
        }
    }

    public override void PhysicsUpdate()
    {
        enemyController.SetVelocityX(0);
    }

    public override void OnExit()
    {
        // 待机后转向
        Vector3 scale = enemyController.transform.localScale;
        enemyController.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}