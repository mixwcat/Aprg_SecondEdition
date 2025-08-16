using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine _stateMachine, Enemy _enemy, string _animBoolName)
        : base(_stateMachine, _enemy, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Additional logic for entering idle state can be added here
    }

    public override void Update()
    {
        base.Update();
        // Logic for updating idle state can be added here
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting idle state can be added here
    }
}
