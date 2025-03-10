using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType SceneType; // 定义这个场景的种类
    public AssetReference sceneReference; // 场景引用
}
