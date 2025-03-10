public class JumpBoostEffect : IFoodEffect
{
    private float boostValue;

    public JumpBoostEffect(float value)
    {
        boostValue = value;
    }

    public void ApplyEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.jumpHeight += boostValue;
    }

    public void RemoveEffect()
    {
        PlayerController instance = PlayerController.Instance;
        if (instance == null) return;
        instance.playerCharacter.playerPara.jumpHeight =
            instance.playerCharacter.templatePlayerPara.jumpHeight;
    }
}