using UnityEngine;
using UnityEngine.AddressableAssets;

public class Initializer : MonoBehaviour
{
    public AssetReference persistent;

    private void Awake()
    {
        Addressables.LoadSceneAsync(persistent);
    }
}
