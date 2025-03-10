using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ManaBoostEffect : IFoodEffect
{
    private float boostValue;

    public ManaBoostEffect(float value)
    {
        boostValue = value;
    }

    public void ApplyEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.maxMana *= boostValue;
    }

    public void RemoveEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.maxMana =
            instance.playerCharacter.templatePlayerPara.maxMana;
    }
}