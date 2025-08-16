using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
        // 可以在这里添加跳跃落地的逻辑，比如播放落地动画
    }

    override public void Update()
    {
        base.Update();
        if (player.IsGroundDetected())
        {
            // 如果检测到地面，则可以切换到站立状态
            player.stateMachine.ChangeState(player.idleState);
        }


        player.Flip();
        player.Move();
    }

    override public void Exit()
    {
        base.Exit();
        // 可以在这里添加离开跳跃状态时的逻辑
    }
}
