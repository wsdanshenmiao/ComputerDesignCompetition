public class UnlockSkillEffect : IFoodEffect
{
    private readonly SkillType _skillType;

    public UnlockSkillEffect(SkillType skillType)
    {
        _skillType = skillType;
    }

    public void ApplyEffect()
    {
        SkillUnlockManager.Instance.UnlockSkill(_skillType);
    }

    public void RemoveEffect()
    {
        SkillUnlockManager.Instance.LockAllSkills();
    }
} 