using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[RequireComponent(typeof(DataDefiantion))]
public class Vine : MonoBehaviour, ISaveable
{
    private CompositeCollider2D vineCollider;
    private Tilemap vineTilemap;
    private Rigidbody2D rb;
    [SerializeField]private SkillInputManager skillInputManager;
    private DataDefiantion dataDef;
    private bool isDestroyed = false;
    
    [Header("藤蔓设置")]
    [SerializeField] private float fadeOutSpeed = 2f; // 藤蔓消失动画速度
    private bool isDestroying = false;

    public SavePriority LoadPriority => SavePriority.Environment;

    private void Awake()
    {
        // 在Awake中初始化基本组件
        vineTilemap = GetComponent<Tilemap>();
        vineCollider = GetComponent<CompositeCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dataDef = GetComponent<DataDefiantion>();
        
        if (vineCollider == null || rb == null)
        {
            SetupCollider();
        }
    }

    private void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    private void Start()
    {
        skillInputManager = SkillInputManager.Instance;
        if (skillInputManager == null)
        {
            Debug.LogError("SkillInputManager is not initialized");
        }
    }

    private void SetupCollider()
    {
        // Composite Collider 2D需要Rigidbody2D
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        vineCollider = gameObject.AddComponent<CompositeCollider2D>();
        vineCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;

        // 设置Tilemap的碰撞器
        var tilemapCollider = GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            tilemapCollider = gameObject.AddComponent<TilemapCollider2D>();
        }
        tilemapCollider.usedByComposite = true;
    }

    private void Update()
    {
        if (isDestroying)
        {
            FadeOut();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (!isInitialized)
        // {
        //     Debug.LogWarning("技能管理器未初始化，无法处理碰撞");
        //     return;
        // }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("检测到玩家");
            if (skillInputManager.SizeChangeSkill.IsEnlarged)
            {
                Debug.Log("检测到变大");
                DestroyVine();
            }
        }
    }

    public void DestroyVine()
    {
        if (!isDestroying && !isDestroyed)
        {
            Debug.Log("vine destory");
            isDestroying = true;
            isDestroyed = true;
            vineCollider.enabled = false;
            AudioManager.PlayAudio(AudioName.VineDestory);

            // 使用Tilemap的边界范围一次性清除所有瓦片
            BoundsInt bounds = vineTilemap.cellBounds;
            vineTilemap.SetTilesBlock(bounds, new TileBase[bounds.size.x * bounds.size.y * bounds.size.z]);

            // 更新Composite Collider
            if (TryGetComponent<TilemapCollider2D>(out var tilemapCollider))
            {
                tilemapCollider.enabled = false;
            }
        }
    }

    private void FadeOut()
    {
        Color currentColor = vineTilemap.color;
        currentColor.a -= Time.deltaTime * fadeOutSpeed;
        vineTilemap.color = currentColor;

        if (currentColor.a <= 0)
        {
            gameObject.SetActive(false); 
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.Vine.ContainsKey(dataDef.dataID))
        {
            isDestroyed = _data.Vine[dataDef.dataID];
            if (isDestroyed)
            {
                gameObject.SetActive(false); 
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.Vine.ContainsKey(dataDef.dataID))
        {
            _data.Vine[dataDef.dataID] = isDestroyed;
        }
        else
        {
            _data.Vine.Add(dataDef.dataID, isDestroyed);
        }
    }
}
