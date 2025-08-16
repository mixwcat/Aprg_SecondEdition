using UnityEngine;

public class PlayerOnGroundState : PlayerState
{
    public PlayerOnGroundState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();

        Subscribe_StateSwitch();
    }



    override public void Update()
    {
        base.Update();
    }

    override public void Exit()
    {
        base.Exit();
        Unsubscribe_StateSwitch();
    }


    private void Subscribe_StateSwitch()
    {
        EventCenter.Subscribe("OnPlayerDash", OnDash);
        EventCenter.Subscribe("OnPlayerJump", OnJump);
        EventCenter.Subscribe("OnPlayerAttack", OnAttack);
        EventCenter.Subscribe("OnPlayerAttackOver", OnAttackOver);
        EventCenter.Subscribe("OnPlayerNextAttackQueued", NextAttackQueued);
        EventCenter.Subscribe("OnPlayerDefend", OnDefend);
    }

    private void Unsubscribe_StateSwitch()
    {
        EventCenter.Unsubscribe("OnPlayerDash", OnDash);
        EventCenter.Unsubscribe("OnPlayerJump", OnJump);
        EventCenter.Unsubscribe("OnPlayerAttack", OnAttack);
        EventCenter.Unsubscribe("OnPlayerAttackOver", OnAttackOver);
        EventCenter.Unsubscribe("OnPlayerNextAttackQueued", NextAttackQueued);
        EventCenter.Unsubscribe("OnPlayerDefend", OnDefend);
    }


    /// <summary>
    /// InputActions里监听到输入事件时调用
    /// </summary>
    /// <param name="parameter"></param>
    private void OnDash(object parameter)
    {
        if (CanSwitchTo(player.dashState))
        {
            player.stateMachine.ChangeState(player.dashState);
        }
    }

    private void OnJump(object parameter)
    {
        if (player == null)
        {
            Debug.LogWarning("Player is null. Skipping OnJump.");
            return;
        }

        if (stateMachine == null)
        {
            Debug.LogWarning("StateMachine is null. Skipping OnJump.");
            return;
        }


        // 检查是否可以跳跃
        if (player.IsGroundDetected() && CanSwitchTo(player.jumpState))
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    private void OnAttack(object parameter)
    {
        if (CanSwitchTo(player.attackState))
            stateMachine.ChangeState(player.attackState);
    }

    private void OnAttackOver(object parameter)
    {
        // 攻击结束后，返回到Idle状态
        stateMachine.ChangeState(player.idleState);
    }

    private void NextAttackQueued(object parameter)
    {
        if (stateMachine.currentState == player.attackState)
        {
            player.isNextAttackQueued = true;
        }
    }

    private void OnDefend(object parameter)
    {
        if (CanSwitchTo(player.defendState))
        {
            stateMachine.ChangeState(player.defendState);
        }
    }



    protected override bool CanSwitchTo(PlayerState newState)
    {
        if (stateMachine.currentState == player.dashState && (newState == player.attackState || newState == player.defendState))
        {
            return false;
        }
        else if (stateMachine.currentState == player.attackState && newState == player.attackState)
        {
            return false;
        }
        return true;
    }



}
