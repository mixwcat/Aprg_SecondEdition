using UnityEngine;

public class PlayerDashState : PlayerOnGroundState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    private int currentFacingDir;
    override public void Enter()
    {
        base.Enter();
        player.dashTimer = player.dashDuration;
        currentFacingDir = player.facingDir;
    }

    override public void Update()
    {
        base.Update();
        player.rb.linearVelocity = new Vector2(currentFacingDir * player.dashSpeed, player.rb.linearVelocity.y);

        player.dashTimer -= Time.deltaTime;
        if (player.dashTimer <= 0)
            player.stateMachine.ChangeState(player.idleState);
    }

    override public void Exit()
    {
        base.Exit();

        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }


}
