using UnityEngine;

public class BossHardCCState : BossBaseState
{
    public BossHardCCState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Boss Stun");
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }

    
}
