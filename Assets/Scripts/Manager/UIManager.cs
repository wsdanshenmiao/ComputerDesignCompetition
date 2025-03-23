using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public CharacterEventSO HealthEvent;
    //public CharacterEventSO ManaEvent;
    public CharacterSO CharacterInfo;
    public PlayerSO PlayerInfo;
    //public PlayerStateBar PlayerState;

    [Header("获取总UI")]
    public GameObject UICanvas;

    private float currentHealth;
    private float currentMana;

    private void OnEnable()
    {
        HealthEvent.OnEventRaised += OnHealthChange;
        //ManaEvent.OnEventRaised += OnManaChange;
    }

    private void OnDisable()
    {
        HealthEvent.OnEventRaised -= OnHealthChange;
        //ManaEvent.OnEventRaised -= OnManaChange;
    }

    // UI血量更改
    public void OnHealthChange(Character character)
    {
        // 获取当前的生命值
        currentHealth = character.CurrHealth;

        float healthPercent = currentHealth / CharacterInfo.maxHealth;
        //PlayerState.ChangeHealth(healthPercent);
    }

    public void OnManaChange(Character character)
    {
        // 获取当前的蓝量
        if (character is PlayerCharacter)
        {
            currentMana = (character as PlayerCharacter).CurrMana;
        }
        float manaPercent = currentMana / PlayerInfo.maxMana;
        //PlayerState.ChangeMana(manaPercent);
    }

    public void OnCloseCanvas()
    {
        UICanvas.SetActive(false);

        // 特殊的子物件要关闭
        foreach (Transform element in UICanvas.transform)
        {
            if (element.gameObject.name == "DialogCanvas")
            {
                element.gameObject.SetActive(false);
            }
        }
    }

    public void OnOpenCanvas()
    {
        UICanvas.SetActive(true);
    }
}
