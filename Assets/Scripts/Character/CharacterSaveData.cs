/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.15
	Finish time:	
	Abstract:       玩家需要储存的数据
****************************************************************************************/


using UnityEngine;

public class CharacterSaveData
{
    public CharacterSO charaPara;
    public float currHP;

    public CharacterSaveData(CharacterSO charaPara, float currHP)
    {
        this.charaPara = MonoBehaviour.Instantiate(charaPara);
        this.currHP = currHP;
    }
}
