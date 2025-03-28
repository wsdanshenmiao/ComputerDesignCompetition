using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [Header("事件监听")]
    public SceneLoadEventSO SceneLoadEvent;
    public VoidEventSO AfterSceneLoadEvent;

    [SerializeField] private PhysicsMaterial2D glossMaterial;
    
    private PlayerInput playerInput;
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rigidBody;

    [HideInInspector] public PlayerCharacter playerCharacter;

    public float inputSign => playerInput.inputAxesX /
        (playerInput.inputAxesX == 0 ? 1 : Mathf.Abs(playerInput.inputAxesX));

    private float originGravity;

    [HideInInspector] public bool isBigSize = false;
    public bool isGround => physicsCheck.isGround;
    public bool touchLeftWall => physicsCheck.touchLeftWall;
    public bool touchRightWall => physicsCheck.touchRightWall;

    [HideInInspector] public bool finishLoadScene = false;
    
    
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        if (physicsCheck == null)
        {
            physicsCheck = GetComponent<PhysicsCheck>();
        }
        playerCharacter = GetComponent<PlayerCharacter>();
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    protected void OnEnable()
    {
        playerInput.EnableGamePlayInput();
        SceneLoadEvent.OnEventRaised += OnSceneLoad;
        AfterSceneLoadEvent.OnEventRaised += AfterSceneLoad;
    }

    protected void Start()
    {
        float scale = playerCharacter.playerPara.originSize;
        transform.localScale = new Vector3(scale, scale, scale);
        originGravity = rigidBody.gravityScale;
    }

    protected void Update()
    {
        //Debug.Log(playerCharacter.IsDeath);
        if (playerInput.changeBackpacker) {
            CraftingSystem.Instance.ChangeCanvasState();
        }

        if (isGround) {
            rigidBody.sharedMaterial = null;
        }
        else {
            rigidBody.sharedMaterial = glossMaterial;
        }
    }

    protected void OnDisable()
    {
        playerInput.DisableGamePlayInput();
        SceneLoadEvent.OnEventRaised -= OnSceneLoad;
        AfterSceneLoadEvent.OnEventRaised -= AfterSceneLoad;
    }

    public void DisableMove()
    {
        GetComponent<PlayerStateMachine>().enabled = false;
        Transform[] child = GetComponentsInChildren<Transform>();
        foreach (Transform t in child)
        {
            t.gameObject.SetActive(false);
        }
        SetGravityScale(0);
        playerInput.DisableGamePlayInput();
    }

    public void EnableMove()
    {
        GetComponent<PlayerStateMachine>().enabled = true;
        Transform[] child = GetComponentsInChildren<Transform>();
        foreach (Transform t in child)
        {
            t.gameObject.SetActive(true);
        }
        SetGravityScale(originGravity);
        playerInput.EnableGamePlayInput();
    }

    public void SetVelocityX(float velocityX)
    {
        if (playerCharacter.canMove)
        {
            rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
        }
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    public void SetGravityScale(float gravityScale)
    {
        rigidBody.gravityScale = gravityScale;
    }

    public float GetVelocityX()
    {
        return rigidBody.velocity.x;
    }

    public float GetVelocityY()
    {
        return rigidBody.velocity.y;
    }

    public float GetGravityScale()
    {
        return rigidBody.gravityScale;
    }

    /// <summary>
    /// 玩家移动，同时控制玩家的左右缩放
    /// </summary>
    /// <param name="speed"></param>
    public void Move(float speed)
    {
        if (!playerCharacter.canMove) return;

        if (playerInput.isMove)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(scale.x) * inputSign, scale.y, scale.z);
        }

        SetVelocityX(speed * playerInput.inputAxesX);
    }

    #region Event
    private void OnChangeSize(bool isBigSize)
    {
        this.isBigSize = isBigSize;
    }

    private void OnSceneLoad(GameSceneSO loadScene, Vector3 arg1, bool arg2)
    {
        playerInput.DisableGamePlayInput(); // 关闭控制系统
        //if (loadScene.SceneType == SceneType.Menu) // 如果是菜单页面才会将动画系统关闭掉
        //    animator.enabled = false; // 关闭动画系统
    }

    private void AfterSceneLoad()
    {
        finishLoadScene = true;
        playerInput.EnableGamePlayInput(); // 恢复控制系统
        //animator.enabled = true; // 恢复动画系统
    }
    #endregion
}
