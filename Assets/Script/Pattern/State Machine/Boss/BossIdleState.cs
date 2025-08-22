using UnityEngine;

public class BossIdleState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossMoveRightHash = Animator.StringToHash("MoveRight");
    readonly int BossMoveForwardtHash = Animator.StringToHash("MoveForward");



    float timeRoadToCaution;

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
        FaceTarget();
        bossStateMachine.Animator.SetFloat(BossMoveRightHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
        bossStateMachine.Animator.SetFloat(BossMoveForwardtHash, 0, bossStateMachine.CrossFadeDuration, deltaTime);
        Move(deltaTime);       

        if (UtilityAIManager.Instance.interruped)
        {
            return;
        }

        ConditionSwitchState(deltaTime);
    }

    public override void Exit()
    {
    }

    void ConditionSwitchState(float deltaTime)
    {
        timeRoadToCaution += deltaTime;
        if (timeRoadToCaution >= 1)
        {
            bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
            return;
        }
    }
}
