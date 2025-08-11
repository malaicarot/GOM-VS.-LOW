using UnityEngine;

public class PlayerStunState : PlayerBaseState
{
    readonly int StunAnimationHash = Animator.StringToHash("Stun");
    readonly string StunAnimationTag = "Stun";
    public PlayerStunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(StunAnimationHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, StunAnimationTag) > 0.8f && GetNormalizedTime(stateMachine.Animator, StunAnimationTag) <= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
    }
}
