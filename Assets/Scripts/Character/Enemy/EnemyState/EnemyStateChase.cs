/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的追击状态
****************************************************************************************/
using UnityEngine;

[CreateAssetMenu(menuName = "State/EnemyState/Chase", fileName = "EnemyChase")]
public class EnemyStateChase : EnemyState
{
    private Vector3 dir;
    private bool isChase = true;

    public override void LogicUpdate()
    {
        // 寻找玩家
        dir = enemyController.FindPlayer();
        if (enemyController.enemyCharacter.IsDeath)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateDeath));
        }
        if (enemyController.enemyCharacter.IsHurt)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateHurt));
        }
        if (dir == Vector3.zero)
        {
            enemyStateMachine.SwitchState(typeof(EnemyStateIdle));
        }
        isChase = enemyController.isGround ? true : false;
    }

    public override void PhysicsUpdate()
    {
        if (isChase && Mathf.Abs(dir.x) > enemyController.enemyCharacter.enemyPara.stopDistance)
        {
            Vector3 scale = enemyController.transform.localScale;
            enemyController.transform.localScale = dir.x > 0 ?
                new Vector3(Mathf.Abs(scale.x), scale.y, scale.z) :
                new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            enemyController.Run();
        }
        else
        {
            enemyController.SetVelocityX(0);
        }
    }
}

