using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    readonly int InAirHash = Animator.StringToHash("InAir");

    Vector3 momentum;

    public PlayerInAirState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(InAirHash, stateMachine.CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateDirection() + momentum, deltaTime);

        if (stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {

    }
}
