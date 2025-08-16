using UnityEngine;

public class Player : Entity
{
    #region 状态机
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerOnGroundState onGroundState { get; private set; }
    public PlayerDefendState defendState { get; private set; }
    public PlayerOnDefendedState onDefendedState { get; private set; }
    #endregion

    #region 组件
    // State
    public PlayerCharacterStats playerStats { get; private set; }
    public PlayerInputActions inputActions { get; private set; }
    #endregion

    #region 参数
    [Header("移动")]
    public Vector2 inputVector { get; private set; }
    public int facingDir;
    public float moveSpeed;


    [Header("冲刺")]
    public float dashSpeed;
    public float dashDuration;
    public float dashTimer;

    [Header("跳跃")]
    public float jumpForce;

    [Header("防御")]
    public bool isDefending;
    public bool isDefendSucceed;

    [Header("攻击")]
    //public int comboCount = 2; // 连击次数
    public int comboResetTime = 1; // 连击重置时间
    public Vector2[] attackVelocity; // 攻击时的位移
    public float attackVelocityDuration = 0.5f; // 攻击位移持续时间
    public bool isNextAttackQueued { get; set; } = false; // 是否有下一个攻击被排队


    #endregion

    #region 实现
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        fallState = new PlayerFallState(stateMachine, this, "Jump");
        attackState = new PlayerAttackState(stateMachine, this, "Attack");
        onGroundState = new PlayerOnGroundState(stateMachine, this, "Idle");
        defendState = new PlayerDefendState(stateMachine, this, "Defending");
        onDefendedState = new PlayerOnDefendedState(stateMachine, this, "OnDefended");

        //获取组件
        playerStats = GetComponent<PlayerCharacterStats>();
        inputActions = GetComponent<PlayerInputActions>();
        PlayerManager.Instance.player = this;

        //订阅移动事件，获取Vector
        //EventCenter.Subscribe("OnPlayerMove", OnMoveInput);
    }


    protected override void Start()
    {
        base.Start();

        // 初始化状态机
        stateMachine.Initialized(idleState);
    }


    protected override void Update()
    {
        // 更新状态机
        stateMachine.currentState.Update();

        // 获取移动输入向量
        inputVector = inputActions.playerInputSystem.Player.Move.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C键被按下");
            SkillManager.Instance.Try_UseSkill<TestSkill_SO>();
        }
    }
    #endregion


    #region 方法
    /// <summary>
    /// 翻转角色
    /// </summary>
    public void Flip()
    {
        // 获取facingDir
        if (inputVector != Vector2.zero)
        {
            facingDir = inputVector.x > 0 ? 1 : -1;
        }

        // 通过facingDir来翻转角色
        if (facingDir == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (facingDir == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        rb.linearVelocity = new Vector2(inputVector.x * moveSpeed, rb.linearVelocity.y);
    }


    public void OnDestroy()
    {
        // 清理状态机
        stateMachine.currentState.Exit();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemyone_CharacterStats enemyStats = collision.GetComponent<Enemyone_CharacterStats>();
            playerStats.DoDamage(enemyStats);
        }
    }
    #endregion

}
