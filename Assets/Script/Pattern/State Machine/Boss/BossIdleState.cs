using UnityEngine;

public class BossIdleState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossSpeedHash = Animator.StringToHash("MovementSpeed");

    public BossIdleState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        bossStateMachine.Animator.SetFloat(BossSpeedHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
        // if (IsInChanseRange())
        // {
        //     bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
        //     return;
        // }
        if (IsInCautiousRange())
        {
            bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
            return;
        }
    }
    public override void Exit()
    {
    }
}
