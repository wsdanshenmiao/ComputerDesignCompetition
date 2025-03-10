/****************************************************************************************
	Author:			EFA
	Versions:		1.0
	Creation time:	
	Finish time:	
	Abstract:       箱子
****************************************************************************************/
/****************************************************************************************
	Author:			danshenmiao
	Versions:		2.0
	Creation time:	2025.1.16
	Finish time:	2025.1.16
	Abstract:       添加保存接口
****************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DataDefiantion))]
public class Box1x1 : MonoBehaviour, ISaveable
{
    protected Rigidbody2D rb;
    [SerializeField] private float pushForce = 2f;
    [SerializeField] protected LayerMask playerLayer;
    private DataDefiantion dataDef;

    public SavePriority LoadPriority => SavePriority.Environment;

    protected void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    protected virtual void Start()
    {
        dataDef = GetComponent<DataDefiantion>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (rb.sharedMaterial == null)
        {
            Debug.LogWarning("设置NoFriction物理材质");
        }
    }

    protected void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
            pushDirection.y = 0;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void LoadData(GameData _data)
    {
        string posXID = "PosX" + dataDef.dataID;
        string posYID = "PosY" + dataDef.dataID;
        string posZID = "PosZ" + dataDef.dataID;
        Vector3 pos = transform.position;

        if (_data.floatDatas.ContainsKey(posXID))
        {
            pos.x = _data.floatDatas[posXID];
            pos.y = _data.floatDatas[posYID];
            pos.z = _data.floatDatas[posZID];
        }
        transform.position = pos;
    }

    public void SaveData(ref GameData _data)
    {
        string posXID = "PosX" + dataDef.dataID;
        string posYID = "PosY" + dataDef.dataID;
        string posZID = "PosZ" + dataDef.dataID;
        Vector3 pos = transform.position;

        if (_data.floatDatas.ContainsKey(posXID))
        {
            _data.floatDatas[posXID] = pos.x;
            _data.floatDatas[posYID] = pos.y;
            _data.floatDatas[posZID] = pos.z;
        }
        else
        {
            _data.floatDatas.Add(posXID, pos.x);
            _data.floatDatas.Add(posYID, pos.y);
            _data.floatDatas.Add(posZID, pos.z);
        }
    }
}