/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的移动控制
****************************************************************************************/
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected PhysicsCheck physicsCheck;
    protected Rigidbody2D rigidBody;

    [HideInInspector] public EnemyCharacter enemyCharacter;

    public bool touchRight => physicsCheck.touchRightWall;
    public bool touchLeft => physicsCheck.touchLeftWall;
    public bool isGround => physicsCheck.isGround;

    protected void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        rigidBody = GetComponent<Rigidbody2D>();
        enemyCharacter = GetComponent<EnemyCharacter>();
    }

    public void SetVelocityX(float velocityX)
    {
        if (enemyCharacter.canMove)
        {
            rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
        }
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    public float GetVelocityX()
    {
        return rigidBody.velocity.x;
    }

    public float GetVelocityY()
    {
        return rigidBody.velocity.y;
    }

    /// <summary>
    /// 敌人的移动
    /// </summary>
    /// <param name="speed"></param>
    public void Move(float speed)
    {
        if (!enemyCharacter.canMove) return;

        float scale = transform.localScale.x;
        float sign = scale / (scale == 0 ? 1 : Mathf.Abs(scale));
        rigidBody.velocity = new Vector3(speed * sign, rigidBody.velocity.y);
    }

    public void Walk()
    {
        Move(enemyCharacter.enemyPara.portalSpeed);
    }

    public void Run()
    {
        Move(enemyCharacter.enemyPara.chaseSpeed);
    }

    /// <summary>
    /// 未发现玩家返回Vector3.zero，否则返回距离向量
    /// </summary>
    /// <returns></returns>
    public Vector3 FindPlayer()
    {
        if (PlayerController.Instance == null ||
            !enemyCharacter.CanChase)
            return Vector3.zero;

        Vector3 dir = PlayerController.Instance.transform.position - transform.position;
        float checkRange = enemyCharacter.enemyPara.checkRange;
        return dir.sqrMagnitude < Mathf.Pow(checkRange, 2) ? dir : Vector3.zero;
    }
}
