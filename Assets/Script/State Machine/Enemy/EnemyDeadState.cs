using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    // readonly int DeathHash = Animator.StringToHash("Enemy_Dying");

    public EnemyDeadState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
        // enemyState.Animator.CrossFadeInFixedTime(DeathHash, enemyState.CrossFadeDuration);
        enemyState.Ragdoll.ToggleRagdoll(true);
        GameObject.Destroy(enemyState.Target);
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }
}
