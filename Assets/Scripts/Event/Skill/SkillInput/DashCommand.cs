using UnityEngine;

public class DashCommand : SkillCommand
{
    public DashCommand(Dash skill, PlayerStateMachine playerStateMachine) 
        : base(skill, playerStateMachine)
    {
    }

    public override void Execute()
    {
        playerStateMachine.SwitchState(typeof(PlayerStateCastDash));
    }
} 