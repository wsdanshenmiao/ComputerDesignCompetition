public static class FoodEffectFactory
{
    public static IFoodEffect CreateEffect(FoodType foodType)
    {
        return foodType switch
        {
            FoodType.Die => new DieEffect(),
            FoodType.JumpBoost_I => new JumpBoostEffect(1.5f),
            FoodType.JumpBoost_II => new JumpBoostEffect(2.0f),
            FoodType.JumpBoost_III => new JumpBoostEffect(2.5f),
            
            FoodType.DashBoost_I => new DashBoostEffect(1.2f),
            FoodType.DashBoost_II => new DashBoostEffect(1.5f),
            FoodType.DashBoost_III => new DashBoostEffect(2.0f),
            
            FoodType.HealthBoost_I => new HealthBoostEffect(1.2f),
            FoodType.HealthBoost_II => new HealthBoostEffect(1.5f),
            FoodType.HealthBoost_III => new HealthBoostEffect(2.0f),
            
            FoodType.Special_BC => new ManaBoostEffect(1.5f),
            FoodType.Special_AAC => new ManaRecoveryEffect(1.5f),

            FoodType.Special_ABC => new UnlockSkillEffect(SkillType.Dash),
            FoodType.Special_AC => new UnlockSkillEffect(SkillType.FireBall),
            FoodType.Special_BBC => new UnlockSkillEffect(SkillType.Magnet),
            FoodType.Special_AAB => new UnlockSkillEffect(SkillType.SizeChangeGrow),
            FoodType.Special_ABB => new UnlockSkillEffect(SkillType.SizeChangeShrink),            
            FoodType.Clone => new UnlockSkillEffect(SkillType.Clone),
            
            _ => null
        };
    }
} 