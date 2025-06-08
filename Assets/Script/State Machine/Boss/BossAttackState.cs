using UnityEngine;

public class BossAttackState : BossBaseState
{
    readonly int AttackAnimationHash = Animator.StringToHash("Attack_1");
    readonly string EnemyAttackTag = "Attack";


    public BossAttackState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        if (GetNormalizedTime(bossStateMachine.Animator, EnemyAttackTag) >= 1f)
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
        }



    }
    public override void Exit()
    {
    }
}
