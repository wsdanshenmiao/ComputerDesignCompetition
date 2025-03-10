using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthBoostEffect : IFoodEffect
{
    private float boostValue;

    public HealthBoostEffect(float value)
    {
        boostValue = value;
    }

    public void ApplyEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.charaPara.maxHealth *= boostValue;
    }

    public void RemoveEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.charaPara.maxHealth =
            instance.playerCharacter.templateCharacterPara.maxHealth;
    }
}