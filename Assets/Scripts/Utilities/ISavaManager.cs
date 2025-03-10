public interface ISaveable
{
    SavePriority LoadPriority { get => SavePriority.Other; }
    void RegisterSaveData() => SaveManager.Instance.RegisterSaveData(this);
    void UnRegitsterSaveData() => SaveManager.Instance?.UnRegitsterSaveData(this);
    void LoadData(GameData _data);
    void SaveData(ref GameData _data);
}