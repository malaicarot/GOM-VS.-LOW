using UnityEngine;

public class BossImpactState : BossBaseState
{
    readonly int EnemyImpactHash = Animator.StringToHash("Boss_Impact");
    float duration = 1f;


    public BossImpactState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(EnemyImpactHash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.hitCount++;

        Debug.Log(bossStateMachine.hitCount);
    }

    public override void Tick(float deltaTime)
    {

        if (bossStateMachine.hitCount >= 4)
        {
            bossStateMachine.SwitchState(new BossCounterState(bossStateMachine));
            return;
        }

        duration -= deltaTime;
        if (duration <= 0f)
        {
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
        }
        Move(deltaTime);
    }
    public override void Exit()
    {
    }
}
