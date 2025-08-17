using UnityEngine;

public class BossChangePhaseProcessState : BossBaseState
{
    readonly int StartChangePhaseAnimationHash = Animator.StringToHash("Start_ChangePhase");
    readonly int EndChangePhaseAnimationHash = Animator.StringToHash("End_ChangePhase");
    readonly string ChangePhaseAnimationTag = "ChangePhase";

    string skillName = "DarkLightning";


    float timeToChangeState = 3f;

    public BossChangePhaseProcessState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(StartChangePhaseAnimationHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        timeToChangeState -= deltaTime;
        FaceTarget();
        Animation();

        if (timeToChangeState <= 0)
        {
            bossStateMachine.SwitchState(new BossMagicAOEState(bossStateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }

    void Animation()
    {
        if (GetNormalizedTime(bossStateMachine.Animator, ChangePhaseAnimationTag) > 0.8f && GetNormalizedTime(bossStateMachine.Animator, ChangePhaseAnimationTag) <= 1f)
        {
            bossStateMachine.UseSkill(skillName, bossStateMachine.BossSkill.GetSkillBaseName(skillName), bossStateMachine.transform);
            bossStateMachine.Animator.CrossFadeInFixedTime(EndChangePhaseAnimationHash, bossStateMachine.CrossFadeDuration);
        }
    }
}
