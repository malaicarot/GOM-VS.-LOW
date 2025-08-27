using UnityEngine;

public class BeastAttackState : BeastBaseState
{
    readonly int BeastRunHash = Animator.StringToHash("Run");

    public BeastAttackState(BeastStateMachine beastStateMachine) : base(beastStateMachine)
    {
    }

    public override void Enter()
    {
        beastStateMachine.Animator.CrossFadeInFixedTime(BeastRunHash, beastStateMachine.CrossFadeDuration);
        // beastStateMachine.AttackDealDamage.SetAttack(50, 50);
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();
        MoveToTarget(deltaTime);
    }

    public override void Exit()
    {
        if (beastStateMachine.Agent != null && beastStateMachine.Agent.isOnNavMesh)
        {
            beastStateMachine.Agent.ResetPath();
            beastStateMachine.Agent.velocity = Vector3.zero;
        }
    }

    void MoveToTarget(float deltaTime)
    {
        if (beastStateMachine.Agent.isOnNavMesh)
        {
            beastStateMachine.Agent.destination = beastStateMachine.Enemy.transform.position;
            Move(beastStateMachine.Agent.desiredVelocity * beastStateMachine.MoveSpeed, deltaTime);
        }
        // beastStateMachine.Agent.velocity = beastStateMachine.Rigidbody.linearVelocity;
    }
}
