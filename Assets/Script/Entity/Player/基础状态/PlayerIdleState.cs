using UnityEngine;
using System.Collections;

public class PlayerIdleState : PlayerOnGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
    }

    override public void Update()
    {
        base.Update();

        if (player.inputVector != Vector2.zero)
        {
            player.stateMachine.ChangeState(player.moveState);
        }


        if (player.isNextAttackQueued)
        {
            player.isNextAttackQueued = false; // 重置标志位
            player.StartCoroutine(NextAttackQueued());
        }
    }

    override public void Exit()
    {
        base.Exit();
    }

    private IEnumerator NextAttackQueued()
    {
        yield return null;
        player.isNextAttackQueued = false; // 重置标志位
        player.stateMachine.ChangeState(player.attackState); // 切换到攻击状态
    }
}
