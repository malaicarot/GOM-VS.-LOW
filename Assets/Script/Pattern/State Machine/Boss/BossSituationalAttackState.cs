
using UnityEngine;
enum AttackType
{
    Counter,
    Approach,
}

public class BossSituationalAttackSate : BossBaseState
{
    readonly int JumpHash = Animator.StringToHash("Jump");
    // readonly int JumpPunchHash = Animator.StringToHash("Jump_Punch");

    Vector3 momentum;
    Vector3 PlayerPosition;
    public BossSituationalAttackSate(BossStateMachine bossStateMachine, string type) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        PlayerPosition = bossStateMachine.Player.transform.position;
        bossStateMachine.ForceReceiver.AddJumpForce(bossStateMachine.BossJumpForce);
        momentum = bossStateMachine.Agent.velocity;
        momentum.y = 0f;
        bossStateMachine.Animator.CrossFadeInFixedTime(JumpHash, bossStateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        ApproachAttack(deltaTime);
        momentum.y -= 2f;
    }

    public override void Exit()
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.ResetPath();
            bossStateMachine.Agent.velocity = Vector3.zero;
        }
    }

    void ApproachAttack(float deltaTime)
    {
        if (bossStateMachine.Agent != null)
        {
            bossStateMachine.Agent.destination = PlayerPosition;
            Move(bossStateMachine.Agent.desiredVelocity.normalized + momentum, deltaTime);

            if (bossStateMachine.ForceReceiver.Movement.y <= 0 || bossStateMachine.Controller.velocity.y <= 0f)
            {
                if (IsInAttackRange())
                {
                    bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
                    return;
                }

                if (IsInChanseRange())
                {
                    bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
                    return;
                }

                if (IsInCautiousRange())
                {
                    bossStateMachine.SwitchState(new BossCautiousState(bossStateMachine));
                    return;
                }
            }
        }

        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
        FaceTarget();
    }
}
