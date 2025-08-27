using UnityEngine;

public class BeastIdleState : BeastBaseState
{
    readonly int IdleAnimationHash = Animator.StringToHash("Idle");
    readonly string IdleAnimationTag = "Idle";

    public BeastIdleState(BeastStateMachine beastStateMachine) : base(beastStateMachine)
    {
    }

    public override void Enter()
    {
        beastStateMachine.Animator.CrossFadeInFixedTime(IdleAnimationHash, beastStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(beastStateMachine.Animator, IdleAnimationTag) >= 0.8f && GetNormalizedTime(beastStateMachine.Animator, IdleAnimationTag) <= 1f)
        {
            beastStateMachine.SwitchState(new BeastAttackState(beastStateMachine));
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {
    }
}
