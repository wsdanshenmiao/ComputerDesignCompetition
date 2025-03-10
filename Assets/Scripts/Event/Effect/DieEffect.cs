using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DieEffect : IFoodEffect
{
    public void ApplyEffect()
    {
        PlayerController player = PlayerController.Instance;
        if (player == null) return;

        GameObject obj = new GameObject();
        Attack attack = obj.AddComponent<Attack>();
        attack.SetAttackData((int)player.playerCharacter.charaPara.maxHealth, -1, 0);
        player.playerCharacter.TakeDamege(attack);
    }

    public void RemoveEffect()
    {

    }
}