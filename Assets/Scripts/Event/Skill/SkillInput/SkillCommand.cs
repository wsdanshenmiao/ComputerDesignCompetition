    using UnityEngine;

public abstract class SkillCommand
{
    protected readonly Skill skill;
    protected readonly PlayerStateMachine playerStateMachine;

    protected SkillCommand(Skill skill, PlayerStateMachine playerStateMachine)
    {
        this.skill = skill;
        this.playerStateMachine = playerStateMachine;
    }

    public abstract void Execute();
} 