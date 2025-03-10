using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ManaRecoveryEffect : IFoodEffect
{
    private float boostValue;

    public ManaRecoveryEffect(float value)
    {
        boostValue = value;
    }

    public void ApplyEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.manaRestorate *= boostValue;
    }

    public void RemoveEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.manaRestorate =
            instance.playerCharacter.templatePlayerPara.manaRestorate;
    }
}