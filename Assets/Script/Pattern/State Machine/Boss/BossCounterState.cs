using UnityEngine;

public class BossCounterState : BossBaseState
{
    readonly int Counter_1Hash = Animator.StringToHash("Counter_1");
    readonly int Counter_2Hash = Animator.StringToHash("Counter_2");
    readonly string EnemyCounterTag = "Counter";

    public BossCounterState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(Counter_1Hash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.hitCount = 0;
    }

    public override void Tick(float deltaTime)
    {
        Counter();
    }

    public override void Exit()
    {
        
    }

    void Counter()
    {
        bossStateMachine.hitCount = 0;
        if (GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) > 0.8f && GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) < 1f)
        {
            // bossStateMachine.Animator.CrossFadeInFixedTime(Counter_2Hash, bossStateMachine.CrossFadeDuration);
            // if (GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) > 0.8f && GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) < 1f)
            // {
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
            // }
        }
    }


}
