using UnityEngine;

public class BeastAttackState : BeastBaseState
{
    readonly int BeastRunHash = Animator.StringToHash("Attack");

    public BeastAttackState(BeastStateMachine beastStateMachine) : base(beastStateMachine)
    {
    }

    public override void Enter()
    {

        beastStateMachine.Animator.CrossFadeInFixedTime(BeastRunHash, beastStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();
        if (beastStateMachine.IsMove)
        {

            MoveToTarget(deltaTime);
        }
        else
        {
            Effect();
        }
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
            beastStateMachine.Agent.destination = PlayerSkill.Instance.target.transform.position;
            Move(beastStateMachine.Agent.desiredVelocity * beastStateMachine.MoveSpeed, deltaTime);
        }
        // beastStateMachine.Agent.velocity = beastStateMachine.Rigidbody.linearVelocity;
    }


    void Effect()
    {
        if (PlayerSkill.Instance.target == null)
        {
            return;
        }
        Vector3 direction = PlayerSkill.Instance.target.transform.position - beastStateMachine.transform.position;
        beastStateMachine.Effect.transform.rotation = Quaternion.LookRotation(direction);
    }
}
