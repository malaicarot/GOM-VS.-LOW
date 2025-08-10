using UnityEngine;

public class BossIdleState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossMoveRightHash = Animator.StringToHash("MoveRight");
    readonly int BossMoveForwardtHash = Animator.StringToHash("MoveForward");

    public BossIdleState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.RandomCombo();
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        bossStateMachine.Animator.SetFloat(BossMoveRightHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
        bossStateMachine.Animator.SetFloat(BossMoveForwardtHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
        Move(deltaTime);

        if (UtilityAIManager.UtilityAIManagerSingleton.interruped)
        {
            return;
        }

        if (IsInCautiousRange())
        {
            bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
            return;
        }

        if (IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }

        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
            return;
        }
    }
    public override void Exit()
    {
    }
}
