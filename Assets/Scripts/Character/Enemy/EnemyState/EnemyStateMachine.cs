/****************************************************************************************
	Author:			danshenmiao
	Versions:		1.0
	Creation time:	2025.1.12
	Finish time:	
	Abstract:       敌人的状态机，用来管理敌人的状态
****************************************************************************************/

using UnityEngine;

public class EnemyStateMachine : BaseStateMachine
{
    [SerializeField] private EnemyState[] enemyStates;

    private new void Awake()
    {
        base.Awake();
        EnemyController enemyController = GetComponent<EnemyController>();
        Animator animator = GetComponent<Animator>();

        // 初始化状态字典
        foreach (var state in enemyStates)
        {
            EnemyState newState = Instantiate(state);
            newState.InitPlayerState(enemyController, this, animator);
            stateTable.Add(newState.GetType(), newState);
        }
    }

    private void Start()
    {
        IState patrolState;
        if (stateTable.TryGetValue(typeof(EnemyStatePatrol), out patrolState))
        {
            SwitchOn(patrolState);
        }
        else
        {
            Debug.LogError("Player StateMachine Have No Idle State");
        }

    }
}
