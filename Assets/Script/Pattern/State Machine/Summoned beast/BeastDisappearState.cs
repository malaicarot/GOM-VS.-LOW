using UnityEngine;

public class BeastDisappearState : BeastBaseState
{
    readonly int DisappearAnimationHash = Animator.StringToHash("Disappear");

    public BeastDisappearState(BeastStateMachine beastStateMachine) : base(beastStateMachine)
    {
    }

    public override void Enter()
    {
        beastStateMachine.Animator.CrossFadeInFixedTime(DisappearAnimationHash, beastStateMachine.CrossFadeDuration);
        beastStateMachine.ReturnBeast();

    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
