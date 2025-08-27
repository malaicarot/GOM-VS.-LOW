using UnityEngine;

public class BeastAppearState : BeastBaseState
{
    readonly int AppearAnimationHash = Animator.StringToHash("Appear");
    readonly string AppearAnimationTag = "Appear";

    public BeastAppearState(BeastStateMachine beastStateMachine) : base(beastStateMachine)
    {
    }

    public override void Enter()
    {
        beastStateMachine.Animator.CrossFadeInFixedTime(AppearAnimationHash, beastStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

        if (GetNormalizedTime(beastStateMachine.Animator, AppearAnimationTag) >= 0.8f && GetNormalizedTime(beastStateMachine.Animator, AppearAnimationTag) <= 1f)
        {
            beastStateMachine.SwitchState(new BeastIdleState(beastStateMachine));
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {
    }
}
