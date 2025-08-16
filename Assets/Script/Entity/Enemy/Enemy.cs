using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine;

    [Header("检测玩家")]
    public Transform playerDetected;
    public LayerMask playerLayer;
    public float playerCheckDistance;

    [Header("获取组件")]
    public Enemyone_CharacterStats characterStats;

    [Header("状态机")]
    public EnemyState idleState;

    protected override void Awake()
    {
        base.Awake();

        //状态机
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(stateMachine, this, "Idle");

        //角色属性
        characterStats = GetComponent<Enemyone_CharacterStats>();
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
}