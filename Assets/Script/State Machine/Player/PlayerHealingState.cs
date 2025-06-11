using UnityEngine;

public class PlayerHealingState : PlayerBaseState
{
    public PlayerHealingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.RecoverHealth(stateMachine.healing);

    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
    }
}
