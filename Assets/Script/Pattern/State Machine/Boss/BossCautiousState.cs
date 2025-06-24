using UnityEngine;

public class BossCautiousState : BossBaseState
{
    readonly int FlexingAnimationHash = Animator.StringToHash("Flexing");
    public BossCautiousState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(FlexingAnimationHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }
        FaceTarget();
        Move(deltaTime);
    }
    public override void Exit()
    {
    }
}
