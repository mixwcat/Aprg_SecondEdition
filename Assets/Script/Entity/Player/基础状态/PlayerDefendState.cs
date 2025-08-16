using UnityEngine;

public class PlayerDefendState : PlayerOnGroundState
{
    public PlayerDefendState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    private bool canTriggerSuperDefense;
    private float superDefenseTimer;
    override public void Enter()
    {
        base.Enter();
        player.isDefending = true;
        superDefenseTimer = 0;

        player.rb.linearVelocity = Vector2.zero; // 防御时停止移动

        EventCenter.Subscribe("OnPlayerDefendExit", OnDefendExit);
    }

    override public void Update()
    {
        base.Update();

        superDefenseTimer += Time.deltaTime;
    }

    override public void Exit()
    {
        base.Exit();
        player.isDefending = false;

        EventCenter.Unsubscribe("OnPlayerDefendExit", OnDefendExit);
    }

    private void DefendSuccess(object parameter)
    {
        if (superDefenseTimer <= .5f && CanSwitchTo(player.onDefendedState))
        {
            player.stateMachine.ChangeState(player.onDefendedState);
        }
    }

    private void OnDefendExit(object obj)
    {
        stateMachine.ChangeState(player.idleState);
    }
}
