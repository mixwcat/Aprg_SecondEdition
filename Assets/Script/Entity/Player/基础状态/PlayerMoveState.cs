using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerOnGroundState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
    }

    override public void Update()
    {
        base.Update();


        if (player.inputVector == Vector2.zero)
        {
            player.stateMachine.ChangeState(player.idleState);
        }


        player.Flip();
        player.Move();
    }


    override public void Exit()
    {
        base.Exit();
    }
}
