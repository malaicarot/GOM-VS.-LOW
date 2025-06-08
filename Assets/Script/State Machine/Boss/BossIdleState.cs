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
        if (IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }
        bossStateMachine.Animator.SetFloat(BossSpeedHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
    }
    public override void Exit()
    {
    }
}
