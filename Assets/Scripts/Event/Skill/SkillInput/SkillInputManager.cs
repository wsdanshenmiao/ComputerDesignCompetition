using UnityEngine;
using UnityEngine.InputSystem;

public class SkillInputManager : Singleton<SkillInputManager>

{
    [SerializeField] public FireBallSkill fireBallSkill;
    [SerializeField] public MagnetSkill magnetSkill;
    [SerializeField] public SizeChange SizeChangeSkill;
    [SerializeField] public Cloned cloneSkill;
    [SerializeField] public Dash dashSkill;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private PlayerStateMachine playerStateMachine;


    protected override void Awake()
    {
        base.Awake();

        playerInput = new PlayerInputController();
        playerInput.Enable();

        /*//放大缩小
        playerInput.GamePlay.Expand.performed += ctx => OnSizeChangeGrow(ctx);
        playerInput.GamePlay.Shrink.performed += ctx => OnSizeChangeShrink(ctx);
        //火球术
        playerInput.GamePlay.FireBall.performed += ctx => OnFireBall(ctx);
        //吸铁石
        playerInput.GamePlay.Magnet.performed += ctx => OnMagnet(ctx);
        //克隆
        playerInput.GamePlay.Copy.performed += ctx => OnClone(ctx);
        //大冲
        playerInput.GamePlay.Flash.performed += ctx => OnDash(ctx);*/
    }

    public void OnFireBall(InputAction.CallbackContext context)
    {
        if (!playerStateMachine.IsCurrentState<PlayerStateCastFireBall>() &&
            !fireBallSkill.IsInCooldown() &&
            SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.FireBall) &&
            SizeChangeSkill.IsNormal)
        {
            new FireBallCommand(fireBallSkill, playerStateMachine).Execute();
            Debug.Log("火球术成功");
        }
        else
        {
            Debug.Log("火球术失败");
            Debug.Log(playerStateMachine.IsCurrentState<PlayerStateCastFireBall>());
            Debug.Log(fireBallSkill.IsInCooldown());
            Debug.Log(SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.FireBall));
            Debug.Log(SizeChangeSkill.IsNormal);
        }
    }

    public void OnMagnet(InputAction.CallbackContext context)
    {
        if (!playerStateMachine.IsCurrentState<PlayerStateCastMagnet>() &&
            !magnetSkill.IsInCooldown() &&
            SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.Magnet) &&
            SizeChangeSkill.IsNormal)
        {
            new MagnetCommand(magnetSkill, playerStateMachine).Execute();
        }
        else
        {
            Debug.Log("吸铁石失败");
            Debug.Log(playerStateMachine.IsCurrentState<PlayerStateCastMagnet>());
            Debug.Log(magnetSkill.IsInCooldown());
            Debug.Log(SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.Magnet));
            Debug.Log(SizeChangeSkill.IsNormal);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!playerStateMachine.IsCurrentState<PlayerStateCastDash>() &&
            !dashSkill.IsInCooldown() &&
            SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.Dash))
        {
            new DashCommand(dashSkill, playerStateMachine).Execute();
        }
    }

    public void OnSizeChangeGrow(InputAction.CallbackContext context)
    {
        if (SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.SizeChangeGrow))
        {
            Debug.Log("放大");
            SizeChangeSkill.EnlargeSize();
        }
        else
        {
            Debug.Log("放大失败");
        }
    }

    public void OnSizeChangeShrink(InputAction.CallbackContext context)
    {
        if (SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.SizeChangeShrink))
        {
            SizeChangeSkill.ShrinkSize();
        }
    }

    public void OnClone(InputAction.CallbackContext context)
    {
        if (!cloneSkill.IsInCooldown() &&
        SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.Clone) &&
        SizeChangeSkill.IsNormal)
        {
            cloneSkill.UseSkill();
        }
        else
        {
            Debug.Log("克隆失败");
            Debug.Log(cloneSkill.IsInCooldown());
            Debug.Log(SkillUnlockManager.Instance.IsSkillUnlocked(SkillType.Clone));
            Debug.Log(SizeChangeSkill.IsNormal);
        }
    }

}