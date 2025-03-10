using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<ISaveable> saveableList = new List<ISaveable>();
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()//删除存档
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataHandler.Delete();
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Path.Combine(Application.persistentDataPath, fileName));
    }

    public bool LoadLastScene()
    {
        gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            NewGame();
        }
        return SceneLoader.Instance.LoadData(gameData);
    }

    public bool SaveLastScene()
    {
        bool save = SceneLoader.Instance.SaveData(ref gameData);
        dataHandler.Save(gameData);
        return save;
    }

    public void NewGame()
    {
        gameData = new GameData();
    }


    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewGame();
        }

        // 根据LoadPriority对列表进行排序
        saveableList.Sort((a, b) => a.LoadPriority.CompareTo(b.LoadPriority));

        foreach (ISaveable saveManager in saveableList)
        {
            saveManager.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        foreach (ISaveable saveManager in saveableList)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    /// <summary>
    /// 将要保存的数据注册
    /// </summary>
    /// <param name="saveable"></param>
    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    /// <summary>
    /// 取消数据的注册
    /// </summary>
    /// <param name="saveable"></param>
    public void UnRegitsterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }

    public bool HasSavedData()
    {
        if (dataHandler.Load() != null)
        {
            return true;
        }

        return false;
    }
}

