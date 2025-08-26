using UnityEngine;

public class EnemyHardCCState : EnemyBaseState
{
    public EnemyHardCCState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
        Debug.Log("Stun");
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }


}
