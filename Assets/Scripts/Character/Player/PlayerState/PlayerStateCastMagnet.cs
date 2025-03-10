using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/CastMagnet", fileName = "PlayerCastMagnet")]
public class PlayerStateCastMagnet : PlayerState
{
    private float castDuration = 1f;
    private float currentCastTime;
    private bool isCasting = false;
    private Skill currentSkill;

    public void SetCurrentSkill(Skill skill)
    {
        currentSkill = skill;
    }

    public override void OnEnter()
    {
        Debug.Log("进入磁铁状态");
        base.OnEnter();
        playerController.playerCharacter.isCast = true;

        if (currentSkill == null)
        {
            Debug.LogError("No skill set for casting!");
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            return;
        }

        // 初始化施法状态
        currentCastTime = 0f;
        isCasting = true;

        // 停止水平移动
        playerController.SetVelocityX(0f);

        // 禁用玩家输入
        playerInput.DisableGamePlayInput();
    }

    public override void LogicUpdate()
    {
        if (!isCasting) return;

        currentCastTime += Time.deltaTime;

        if (currentCastTime >= castDuration)
        {
            //TODO 设置音效
            currentSkill.UseSkill();
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        // 保持垂直方向的物理效果(重力)
        // 但水平方向速度保持为0
        playerController.SetVelocityX(0f);
    }

    public override void OnExit()
    {
        // 恢复玩家输入
        playerInput.EnableGamePlayInput();

        // 重置施法状态
        isCasting = false;
        playerController.playerCharacter.isCast = false;
        currentCastTime = 0f;
        currentSkill = null;  // 清除当前技能引用
    }

    // 提供一个公共方法用于外部打断施法
    public void InterruptCast()
    {
        if (isCasting)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
        }
    }
}