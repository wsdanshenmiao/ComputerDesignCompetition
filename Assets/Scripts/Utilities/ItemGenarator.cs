using UnityEngine;  
using UnityEditor;  

public class ItemGenarator : MonoBehaviour  
{  
    [ContextMenu("Create Scriptable Object")]  
    public void CreateScriptableObject()
    {
        string[] finalItem = new[] { "ChineseMagicMirror", "Compass", "Gunpowder" };
        int index = 0;
        
        for (int i = 0; i < (int)Item.ItemType.ItemTypeCount; ++i) {
            // 创建新的 ScriptableObject 实例  
            ItemScriptableObject myData = ScriptableObject.CreateInstance<ItemScriptableObject>();
            myData.itemType = (Item.ItemType)i;
            myData.itemName = myData.itemType.ToString();

            // 生成文件路径  
            Debug.Log("Created ScriptableObject");
            string path = "Assets/Data SO/CraftingSystem/Items/" + finalItem[index] + "/" + myData.itemName + ".asset";  
        
            // 创建并保存资产  
            AssetDatabase.CreateAsset(myData, path);  
            AssetDatabase.SaveAssets();  
            EditorUtility.FocusProjectWindow();  
            Selection.activeObject = myData; // 选中新的资源

            for (int j = 0; j < finalItem.Length; ++j) {
                if (finalItem[j] == myData.itemName) {
                    index++;
                }
            }

            // 打印信息  
            Debug.Log($"Created ScriptableObject at {path}");  
        }
    }  
}  
