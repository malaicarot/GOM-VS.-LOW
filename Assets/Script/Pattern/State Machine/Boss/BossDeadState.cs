using UnityEngine;

public class BossDeadState : BossBaseState
{
    public BossDeadState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Ragdoll.ToggleRagdoll(true);
        GameObject.Destroy(bossStateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }
}
