using UnityEngine;

public class BossImpactState : BossBaseState
{
    readonly int EnemyImpactHash = Animator.StringToHash("Boss_Impact");
    float duration = .5f;

    public BossImpactState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(EnemyImpactHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0f)
        {
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
        }
    }
    public override void Exit()
    {
    }
}
