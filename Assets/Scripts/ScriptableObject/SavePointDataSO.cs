using UnityEngine.AddressableAssets;
using UnityEngine;

[CreateAssetMenu(menuName = "SavePoint")]
public class SavePointSO : ScriptableObject
{
    public string savePointId;
    public bool isActivated;
    public Vector3 pos;
}
