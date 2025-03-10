using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DataDefiantion))]
public class Ingredient : InteractableObject, I_Interactable, ISaveable
{
    [SerializeField] private ItemData_Ingredient ingredientData;
    private SpriteRenderer spriteRenderer;
    private DataDefiantion dataDef;
    private bool isCollected = false;

    public SavePriority LoadPriority => SavePriority.Environment;

    protected override void Start()
    {
        base.Start();
        dataDef = GetComponent<DataDefiantion>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // if(spriteRenderer != null && ingredientData != null)
        // {
        //     spriteRenderer.sprite = ingredientData.icon;
        // }
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
            //BackPack.Instance.AddIngredient(ingredientData, 1);
            isCollected = true;
            gameObject.SetActive(false); 
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.Ingredients.ContainsKey(dataDef.dataID))
        {
            isCollected = _data.Ingredients[dataDef.dataID];
            if (isCollected)
            {
                gameObject.SetActive(false); 
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        Debug.Log("SaveData: " + dataDef.dataID);
        if (_data.Ingredients.ContainsKey(dataDef.dataID))
        {
            _data.Ingredients[dataDef.dataID] = isCollected;
        }
        else
        {
            _data.Ingredients.Add(dataDef.dataID, isCollected);
        }
    }
}
