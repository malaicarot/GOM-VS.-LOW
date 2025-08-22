using UnityEngine;

public class BossBallisticsState : BossBaseState
{
    readonly int StartShootAnimationHash = Animator.StringToHash("StartShoot");
    // readonly string StartShootAnimationTag = "StartShoot";
    string skillName = "DarkBullet";
    float timeToWait = 2f;



    public BossBallisticsState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.countBallisticsAttack++;
        bossStateMachine.Animator.CrossFadeInFixedTime(StartShootAnimationHash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.UseSkill(skillName, bossStateMachine.BossSkill.GetSkillBaseName(skillName), bossStateMachine.Projectile);
    }

    public override void Tick(float deltaTime)
    {

        FaceTarget();

        if (bossStateMachine.countBallisticsAttack <= bossStateMachine.TimeToShootBullet)
        {
            timeToWait -= deltaTime;
            if (timeToWait <= 0)
            {
                bossStateMachine.SwitchState(new BossBallisticsState(bossStateMachine));
                return;
            }
        }
        else
        {
            bossStateMachine.countBallisticsAttack = 0;
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
}
