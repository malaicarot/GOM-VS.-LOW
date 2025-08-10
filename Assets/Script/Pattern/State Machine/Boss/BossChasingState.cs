using UnityEngine;

public class BossChasingState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossMoveRightHash = Animator.StringToHash("MoveRight");
    readonly int BossMoveForwardtHash = Animator.StringToHash("MoveForward");


    float attackTime;

    public BossChasingState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        attackTime = 0;
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
    }
    
    public override void Tick(float deltaTime)
    {
        bossStateMachine.Animator.SetFloat(BossMoveRightHash, 1, bossStateMachine.CrossFadeDuration, deltaTime);
        bossStateMachine.Animator.SetFloat(BossMoveForwardtHash, 1, bossStateMachine.CrossFadeDuration, deltaTime);

        FaceTarget();
        MoveToPlayer(deltaTime);
        ConditionSwitchState(deltaTime);
    }

    public override void Exit()
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.ResetPath();
            bossStateMachine.Agent.velocity = Vector3.zero;
        }
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

    void ConditionSwitchState(float deltaTime)
    {
        attackTime += deltaTime;

        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
            return;
        }

        if (attackTime >= 3f)
        {
            bossStateMachine.SwitchState(new BossSituationalAttackSate(bossStateMachine, "Approach"));
            return;
        }
    }
}
