using UnityEngine;

public class BossChasingState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");

    public BossChasingState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {

        bossStateMachine.Animator.SetFloat(MovementSpeedHash, 1, bossStateMachine.CrossFadeDuration, deltaTime);
        FaceTarget();
        MoveToPlayer(deltaTime);
        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
            return;
        }
        if (!IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
            return;
        }
        Debug.Log("Chasing");
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
