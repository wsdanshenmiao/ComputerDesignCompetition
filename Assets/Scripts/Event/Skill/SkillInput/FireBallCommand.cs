using UnityEngine;

public class FireBallCommand : SkillCommand
{
    public FireBallCommand(FireBallSkill skill, PlayerStateMachine playerStateMachine) 
        : base(skill, playerStateMachine)
    {
    }

    public override void Execute()
    {
        var castState = playerStateMachine.GetState<PlayerStateCastFireBall>();
        castState.SetCurrentSkill(skill);
        playerStateMachine.SwitchState(typeof(PlayerStateCastFireBall));
    }
} 