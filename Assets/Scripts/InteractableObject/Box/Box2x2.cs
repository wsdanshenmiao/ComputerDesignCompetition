/****************************************************************************************
    Author:         EFA
    Versions:       1.0
    Creation time:  
    Finish time:    
    Abstract:       箱子
****************************************************************************************/
/****************************************************************************************
    Author:         danshenmiao
    Versions:       2.0
    Creation time:  2025.1.16
    Finish time:    2025.1.16
    Abstract:       添加保存接口
****************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DataDefiantion))]
public class PushableBox : MonoBehaviour, ISaveable
{
    public BoolEventSO OnPlayerSizeChanged;
    private Rigidbody2D rb;
    private bool isPlayerEnlarged = false;

    [SerializeField] private float pushForce = 2f;
    [SerializeField] private LayerMask playerLayer;
    private DataDefiantion dataDef;

    public SavePriority LoadPriority => SavePriority.Environment;

    private void Start()
    {
        dataDef = GetComponent<DataDefiantion>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    private void OnEnable()
    {
        OnPlayerSizeChanged.OnEventRaised += HandlePlayerSizeChange;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        OnPlayerSizeChanged.OnEventRaised -= HandlePlayerSizeChange;
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    private void HandlePlayerSizeChange(bool isEnlarged)
    {
        isPlayerEnlarged = isEnlarged;
        // 根据状态更新物理约束
        rb.constraints = isEnlarged ?
            RigidbodyConstraints2D.FreezeRotation :
            RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isPlayerEnlarged) return;

        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
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