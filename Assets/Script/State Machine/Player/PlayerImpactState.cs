using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    readonly int PlayerImpactHash = Animator.StringToHash("Impact");
    float duration = 1f;


    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PlayerImpactHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }
    public override void Exit()
    {
    }
}
