using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/CastDash", fileName = "PlayerCastDash")]
public class PlayerStateCastDash : PlayerState
{
    private float moveDirection;         // 记录进入状态时的朝向

    public override void OnEnter()
    {
        base.OnEnter();

        //TODO(audio) 增加冲刺音效
        // 记录进入状态时的朝向
        moveDirection = playerController.inputSign;

        // TODO 播放变形动画

        // 设置重力系数 要用再改吧
        //playerController.rigidBody.gravityScale = DashGravityScale;

        // 禁用玩家输入
        playerInput.enabled = false;

    }

    public override void OnExit()
    {
        base.OnExit();

        // 恢复正常形态
        playerAnimator.SetTrigger("ToNormal");

        // 恢复默认重力
        //playerController.rigidBody.gravityScale = 1f;

        // 重新启用玩家输入
        playerInput.enabled = true;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 检查是否需要退出状态
        if (playerInput.isJump || playerController.touchLeftWall || playerController.touchRightWall)
        {
            playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            return;
        }

        float speed = SkillInputManager.Instance.dashSkill.DashSpeed;
        // 设置水平速度
        playerController.SetVelocityX(moveDirection * speed);
    }

}
