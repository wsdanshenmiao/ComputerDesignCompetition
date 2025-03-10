/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.15
	Finish time:	
	Abstract:       玩家需要储存的数据
****************************************************************************************/


using UnityEngine;

public class EnemySaveData
{
    public PlayerSO playerPara;
    public float currMana;

    public EnemySaveData(PlayerSO playerPara, float currMana)
    {
        this.playerPara = MonoBehaviour.Instantiate(playerPara);
        this.currMana = currMana;
    }
}
