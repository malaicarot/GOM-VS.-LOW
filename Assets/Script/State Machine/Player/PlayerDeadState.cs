using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    // readonly int DeathHash = Animator.StringToHash("Dying");
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        // stateMachine.Animator.CrossFadeInFixedTime(DeathHash, stateMachine.CrossFadeDuration);
        stateMachine.Ragdoll.ToggleRagdoll(true);
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }
}
