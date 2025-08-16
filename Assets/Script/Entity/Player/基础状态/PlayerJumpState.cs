using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
        player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
    }

    override public void Update()
    {
        base.Update();

        if (player.rb.linearVelocityY <= 0)
        {
            player.stateMachine.ChangeState(player.fallState);
        }


        player.Flip();
        player.Move();
    }

    override public void Exit()
    {
        base.Exit();
    }
}