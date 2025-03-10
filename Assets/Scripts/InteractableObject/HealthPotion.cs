using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DataDefiantion))]
public class HealthPotion : InteractableObject, I_Interactable, ISaveable
{
    private DataDefiantion dataDef;
    private bool isCollected = false;

    public SavePriority LoadPriority => SavePriority.Environment;

    protected override void Start() 
    {
        base.Start();
        dataDef = GetComponent<DataDefiantion>();
    }

    private void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    public void TriggerAction()
    {
        if (!isCollected)
        {
                    if (PlayerController.Instance == null) return;

            PlayerCharacter playerChara = PlayerController.Instance.playerCharacter;
            if (playerChara.CurrHealth <= playerChara.charaPara.maxHealth)
            {
                playerChara.CurrHealth += 30;
                if (playerChara.CurrHealth > playerChara.charaPara.maxHealth)
                {
                    playerChara.CurrHealth = playerChara.charaPara.maxHealth;
                }
                Destroy(gameObject);
            }
            isCollected = true;
            gameObject.SetActive(false); 
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.HealthPotion.ContainsKey(dataDef.dataID))
        {
            isCollected = _data.HealthPotion[dataDef.dataID];
            if (isCollected)
            {
                gameObject.SetActive(false); 
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.HealthPotion.ContainsKey(dataDef.dataID))
        {
            _data.HealthPotion[dataDef.dataID] = isCollected;
        }
        else
        {
            _data.HealthPotion.Add(dataDef.dataID, isCollected);
        }
    }
}
