using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Respawn.RespawnPlayer();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.HandleRespawnState();
    }
    public override void Exit()
    {

    }
}
