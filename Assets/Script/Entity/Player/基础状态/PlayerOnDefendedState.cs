using UnityEngine;

public class PlayerOnDefendedState : PlayerDefendState
{
    public PlayerOnDefendedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
    }

    override public void Update()
    {
        base.Update();
        player.Flip();
    }

    override public void Exit()
    {
        base.Exit();
        player.isDefending = false;
    }
}
