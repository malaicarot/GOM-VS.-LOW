using UnityEngine;

public class BossDeadState : BossBaseState
{
    readonly int DeathAnimationHash = Animator.StringToHash("Death");
    readonly string DeathAnimationTag = "Death";
    public BossDeadState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        // bossStateMachine.Ragdoll.ToggleRagdoll(true);
        // GameObject.Destroy(bossStateMachine.Target);
        bossStateMachine.Animator.CrossFadeInFixedTime(DeathAnimationHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(bossStateMachine.Animator, DeathAnimationTag) > 8f && GetNormalizedTime(bossStateMachine.Animator, DeathAnimationTag) <= 1f)
        {
            Debug.Log("Kill Boss!");
        }
    }
    public override void Exit()
    {
    }
}
