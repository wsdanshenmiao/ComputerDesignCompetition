using UnityEngine;

public class MagnetCommand : SkillCommand
{
    public MagnetCommand(MagnetSkill skill, PlayerStateMachine playerStateMachine) 
        : base(skill, playerStateMachine)
    {
    }

    public override void Execute()
    {
        var castState = playerStateMachine.GetState<PlayerStateCastMagnet>();
        castState.SetCurrentSkill(skill);
        playerStateMachine.SwitchState(typeof(PlayerStateCastMagnet));
    }
} 