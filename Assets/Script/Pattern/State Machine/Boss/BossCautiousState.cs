using UnityEngine;
using UnityEngine.AI;

public class BossCautiousState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossMoveRightHash = Animator.StringToHash("MoveRight");
    readonly int BossMoveForwardtHash = Animator.StringToHash("MoveForward");
    const float AnimationDamping = 0.1f;

    Vector3 targetPosition;
    float changeTime;
    float moveTime;
    float radius = 8f;
    public BossCautiousState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        changeTime = 0f;
        moveTime = 0f;
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
        targetPosition = GetRandomNavmeshPosition(bossStateMachine.transform.position);
    }

    public override void Tick(float deltaTime)
    {
        UpdateAnimation(deltaTime);
        PickNewNavMeshPosition(deltaTime);
        CoditionSwitchState(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.ResetPath();
            bossStateMachine.Agent.velocity = Vector3.zero;
        }
    }

    void UpdateAnimation(float deltatime)
    {
        Vector3 velocity = bossStateMachine.transform.InverseTransformDirection(bossStateMachine.Agent.velocity).normalized;

        float xValue = velocity.x > 0 ? 1 : -1;
        float yValue = velocity.z > 0 ? 1 : -1;

        bossStateMachine.Animator.SetFloat(BossMoveRightHash, xValue, AnimationDamping, deltatime);
        bossStateMachine.Animator.SetFloat(BossMoveForwardtHash, yValue, AnimationDamping, deltatime);

    }

    Vector3 GetRandomNavmeshPosition(Vector3 origin)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return origin;
    }

    void PickNewNavMeshPosition(float deltaTime)
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.destination = targetPosition;
            Move(bossStateMachine.Agent.desiredVelocity.normalized * bossStateMachine.MoveSpeed, deltaTime);
        }
        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
    }

    void CoditionSwitchState(float deltaTime)
    {
        changeTime += deltaTime;
        moveTime += deltaTime;

        if (IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }

        if (moveTime >= 1.5f)
        {
            targetPosition = GetRandomNavmeshPosition(bossStateMachine.transform.position);
            moveTime = 0f;
        }

        if (changeTime >= 6f)
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }
    }
}
