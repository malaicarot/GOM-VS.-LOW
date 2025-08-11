using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        // stateMachine.Targeter.enabled = false;
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {

    }
}
