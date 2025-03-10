/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.15
	Finish time:	
	Abstract:       对象的类型及ID
****************************************************************************************/
using UnityEngine;

public class DataDefiantion : MonoBehaviour
{
    public enum PersistentType
    {
        READWRITE, NOPERSISTENT
    }

    public PersistentType persistentType = PersistentType.READWRITE;
    public string dataID;

    private void OnValidate()
    {
        if (persistentType == PersistentType.READWRITE)
        {
            if (dataID == string.Empty)
            {
                dataID = System.Guid.NewGuid().ToString() + Random.value.ToString();
            }
        }
        else
        {
            dataID = string.Empty;
        }
    }
}