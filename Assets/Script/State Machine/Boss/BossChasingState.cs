using UnityEngine;

public class BossChasingState : BossBaseState
{
    // readonly int FlexAnimationHash = Animator.StringToHash("Flexing");
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");

    public BossChasingState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        // bossStateMachine.Animator.CrossFadeInFixedTime(FlexAnimationHash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
            return;
        }
        bossStateMachine.Animator.SetFloat(MovementSpeedHash, 2, bossStateMachine.CrossFadeDuration, deltaTime);
        FaceTarget();
        MoveToPlayer(deltaTime);
    }
    public override void Exit()
    {
        bossStateMachine.Agent.ResetPath();
        bossStateMachine.Agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltaTime)
    {
        if (bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.destination = bossStateMachine.Player.transform.position;
            Move(bossStateMachine.Agent.desiredVelocity.normalized * bossStateMachine.MoveSpeed, deltaTime);
        }
        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
    }
}
