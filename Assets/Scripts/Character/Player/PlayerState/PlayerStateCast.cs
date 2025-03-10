using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Cast", fileName = "PlayerCast")]
public class PlayerStateCast : PlayerState
{
    private float originGravity;
    private float castDuration = 1f;// 施法持续时间
    private float currentCastTime;// 当前施法时间
    private bool isCasting = false; // 是否正在施法
    private Skill currentSkill;  // 当前正在释放的技能

    public void SetCurrentSkill(Skill skill)
    {
        currentSkill = skill;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerController.playerCharacter.isCast = true;

        if (currentSkill == null)
        {
            Debug.LogError("No skill set for casting!");
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            return;
        }
        // 保存原始重力
        originGravity = playerController.GetGravityScale();

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