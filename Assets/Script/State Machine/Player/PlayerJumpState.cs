using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    readonly int JumpHash = Animator.StringToHash("Jump");
    Vector3 momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Stamina.ReduceStamina(stateMachine.jumpStaminaReduce);
        stateMachine.ForceReceiver.AddJumpForce(stateMachine.JumpForce);
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateDirection() + momentum, deltaTime);
        if (stateMachine.ForceReceiver.Movement.y <= 0 || stateMachine.Controller.velocity.y <= 0f)
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
