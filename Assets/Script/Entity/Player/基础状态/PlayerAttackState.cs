using UnityEngine;
using System.Collections;

public class PlayerAttackState : PlayerOnGroundState
{
    public PlayerAttackState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    private int comboIndex = 1;
    private int comboCount = 2;
    private float lastTimeAttacked = 0;  // 上次攻击的时间，用于长时间不攻击重置连击
    private float timer;  // 用于计时攻击持续时间


    override public void Enter()
    {
        base.Enter();
        timer = 0;

        // 长时间不攻击，重置combo
        ComboReset();

        // 更新Index
        GenerateIndex();

        // 攻击时，速度变为0
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);

        // 攻击时位移
        ApplyAttackVelocity();
    }


    override public void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        // 控制位移距离
        StopAttackVelocity();
    }


    override public void Exit()
    {
        base.Exit();
    }


    private void ApplyAttackVelocity()
    {
        // 应用攻击时的位移
        player.rb.linearVelocity = player.attackVelocity[comboIndex - 1] * player.facingDir;
    }


    private void StopAttackVelocity()
    {
        if (timer > player.attackVelocityDuration)
        {
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
        }
    }


    private void GenerateIndex()
    {
        player.anim.SetInteger("ComboIndex", comboIndex);
        comboIndex += 1;

        if (comboIndex > comboCount)
        {
            comboIndex = 1;
        }
    }


    private void ComboReset()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = 1;
        }
        lastTimeAttacked = Time.time;
    }
}
