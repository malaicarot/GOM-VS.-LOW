using UnityEngine;

public class BossMagicAOEState : BossBaseState
{
    readonly int StartMagicAnimationHash = Animator.StringToHash("Start_AOE");
    readonly int LoopMagicAnimationHash = Animator.StringToHash("Loop_AOE");
    readonly string MagicAnimationTag = "AOE";

    float increaseHeight = 6f;
    float floatSpeed = 2f;
    bool reachedHeight = false;
    float targetHeight;
    string skillName = "MagicalExplosion";

    float timeWaitToCast;
    float countTimeCast;



    public BossMagicAOEState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        countTimeCast = 0;
        timeWaitToCast = bossStateMachine.TimeWaitToCastAOE;
        targetHeight = bossStateMachine.transform.position.y + increaseHeight;
        bossStateMachine.Agent.enabled = false;
        bossStateMachine.Animator.CrossFadeInFixedTime(StartMagicAnimationHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (countTimeCast <= bossStateMachine.TimeCastAOE)
        {
            Vector3 bossPosition = bossStateMachine.transform.position;
            Animation();

            if (!reachedHeight)
            {
                bossPosition.y = Mathf.MoveTowards(bossPosition.y, targetHeight, floatSpeed * deltaTime);
                if (Mathf.Abs(bossPosition.y - targetHeight) < 0.01f)
                {
                    reachedHeight = true;
                }
            }
            else
            {
                timeWaitToCast -= deltaTime;
                if (timeWaitToCast <= 0)
                {
                    CastSkill();
                }
            }

            bossStateMachine.transform.position = bossPosition;
            FaceTarget();
        }
        else
        {
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
        }

    }

    public override void Exit()
    {
        bossStateMachine.Agent.enabled = true;
        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
        }
    }

    void Animation()
    {
        if (GetNormalizedTime(bossStateMachine.Animator, MagicAnimationTag) > 0.8f && GetNormalizedTime(bossStateMachine.Animator, MagicAnimationTag) <= 1f)
        {
            bossStateMachine.Animator.CrossFadeInFixedTime(LoopMagicAnimationHash, bossStateMachine.CrossFadeDuration);
        }
    }

    void CastSkill()
    {
        bossStateMachine.UseSkill(skillName, bossStateMachine.BossSkill.GetSkillBaseName(skillName), bossStateMachine.Player.transform);
        timeWaitToCast = bossStateMachine.TimeWaitToCastAOE;
        countTimeCast++;
    }
}
