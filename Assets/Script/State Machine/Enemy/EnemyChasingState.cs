using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    readonly int EnemyLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int EnemySpeedHash = Animator.StringToHash("Speed");
    const float AnimationDamping = .1f;

    public EnemyChasingState(EnemyStateMachine enemyState) : base(enemyState) { }

    public override void Enter()
    {
        enemyState.Animator.CrossFadeInFixedTime(EnemyLocomotionHash, enemyState.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChanseRange())
        {
            enemyState.SwitchState(new EnemyIdleState(enemyState));
            return;
        }
        MoveToPlayer(deltaTime);
        enemyState.Animator.SetFloat(EnemySpeedHash, 1, AnimationDamping, deltaTime);

    }
    public override void Exit()
    {
        enemyState.NavMeshAgent.ResetPath();
        enemyState.NavMeshAgent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltaTime)
    {
        enemyState.NavMeshAgent.destination = enemyState.Player.transform.position; // vị trí mà enemy hướng tớitới
        Move(enemyState.NavMeshAgent.desiredVelocity.normalized * enemyState.EnemySpeed, deltaTime); // desiredVelocity => thể hiện hướng đi tiếp theo
        enemyState.NavMeshAgent.velocity = enemyState.EnemyController.velocity;
    }

}
