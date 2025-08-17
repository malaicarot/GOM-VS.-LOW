using UnityEngine;
using UnityEngine.AI;

public class BossCautiousState : BossBaseState
{
    readonly int BossLocomotionHash = Animator.StringToHash("Locomotion");
    readonly int BossMoveRightHash = Animator.StringToHash("MoveRight");
    readonly int BossMoveForwardtHash = Animator.StringToHash("MoveForward");
    const float AnimationDamping = 0.1f;

    Vector3 targetPosition;
    float radius = 8f;

    float timeToSwitchState = 0;
    public BossCautiousState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
        targetPosition = GetRandomNavmeshPosition(bossStateMachine.transform.position);
    }

    public override void Tick(float deltaTime)
    {
        timeToSwitchState += deltaTime;
        UpdateAnimation(deltaTime);
        PickNewNavMeshPosition(deltaTime);
        CoditionSwitchState();
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

    void CoditionSwitchState()
    {
        if (timeToSwitchState >= 2)
        {
            int stateNumber = UtilityAIManager.Instance.RandomState();
            if (stateNumber == 1)
            {
                bossStateMachine.SwitchState(new BossBallisticsState(bossStateMachine));
                return;
            }
            else if (stateNumber == 2)
            {
                bossStateMachine.SwitchState(new BossJumpAttackSate(bossStateMachine));
                return;
            }
            else if (stateNumber == 3)
            {
                bossStateMachine.SwitchState(new BossApproachState(bossStateMachine));
                return;
            }
        }
    }
}
