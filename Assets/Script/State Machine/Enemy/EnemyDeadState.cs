using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{

    public EnemyDeadState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
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
