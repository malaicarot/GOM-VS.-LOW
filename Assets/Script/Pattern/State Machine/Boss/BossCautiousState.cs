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
        bossStateMachine.Animator.CrossFadeInFixedTime(BossLocomotionHash, bossStateMachine.CrossFadeDuration);
        changeTime = 0f;
        moveTime = 0f;
        targetPosition = GetRandomNavmeshPosition(bossStateMachine.transform.position);
    }

    public override void Tick(float deltaTime)
    {
        changeTime += deltaTime;
        moveTime += deltaTime;


        UpdateAnimation(deltaTime);
        PickNewNavMeshPosition(deltaTime);


        if (IsInChanseRange())
        {
            bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
            return;
        }

        if (changeTime >= 1.5f)
        {
            bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
        }

        // if (changeTime >= 6f)
        // {
        //     bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
        //     return;
        // }

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

        
        

        Debug.Log(velocity);
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
            // targetPosition = GetRandomNavmeshPosition(bossStateMachine.transform.position);
            // bossStateMachine.Agent.SetDestination(targetPosition);
            bossStateMachine.Agent.destination = targetPosition;
            Move(bossStateMachine.Agent.desiredVelocity.normalized * bossStateMachine.MoveSpeed, deltaTime);

        }
        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
    }
}
