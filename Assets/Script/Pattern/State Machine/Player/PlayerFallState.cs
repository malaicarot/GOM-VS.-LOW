using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    readonly int FallHash = Animator.StringToHash("Fall");
    Vector3 momentum;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateDirection() + momentum, deltaTime);
        if (stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {
    }
}
