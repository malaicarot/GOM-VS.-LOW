using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    readonly int EnemyLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int EnemySpeedHash = Animator.StringToHash("Speed");


    const float AnimationDamping = .1f;
    public EnemyIdleState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
        enemyState.Animator.CrossFadeInFixedTime(EnemyLocomotionHash, enemyState.CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChanseRange())
        {
            enemyState.SwitchState(new EnemyChasingState(enemyState));
            return;
        }

        FaceTarget();
        enemyState.Animator.SetFloat(EnemySpeedHash, 0, AnimationDamping, deltaTime);
    }

    public override void Exit()
    {

    }
}
