using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DashBoostEffect : IFoodEffect
{
    private float boostValue;

    public DashBoostEffect(float value)
    {
        boostValue = value;
    }

    public void ApplyEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.sprintDuration *= boostValue;
    }

    public void RemoveEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.sprintDuration =
            instance.playerCharacter.templatePlayerPara.sprintDuration;
    }
}