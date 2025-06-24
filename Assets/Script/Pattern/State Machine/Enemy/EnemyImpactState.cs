using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    readonly int EnemyImpactHash = Animator.StringToHash("Enemy_Impact");

    float duration = 1f;

    public EnemyImpactState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
        enemyState.Animator.CrossFadeInFixedTime(EnemyImpactHash, enemyState.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0f)
        {
            enemyState.SwitchState(new EnemyIdleState(enemyState));
        }

    }
    public override void Exit()
    {
    }
}
