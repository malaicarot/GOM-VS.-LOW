
using UnityEngine;

public class BossAccumulateAttackSate : BossBaseState
{
    readonly int JumpHash = Animator.StringToHash("Jump");
    Vector3 momentum;
    Vector3 PlayerPosition;


    public BossAccumulateAttackSate(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {

    }

    public override void Enter()
    {
        FaceTarget();
        bossStateMachine.Animator.CrossFadeInFixedTime(JumpHash, bossStateMachine.CrossFadeDuration);
        PlayerPosition = bossStateMachine.Player.transform.position;
        bossStateMachine.ForceReceiver.AddJumpForce(bossStateMachine.BossJumpForce);
        momentum = bossStateMachine.Agent.velocity;
        momentum.y = 0f;
    }

    public override void Tick(float deltaTime)
    {

        AccumulateAttack(deltaTime);
        momentum.y -= 2f;
        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
        }
    }

    public override void Exit()
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.ResetPath();
            bossStateMachine.Agent.velocity = Vector3.zero;
        }
    }

    void AccumulateAttack(float deltaTime)
    {
        if (bossStateMachine.Agent != null)
        {
            bossStateMachine.Agent.destination = PlayerPosition;
            Move(bossStateMachine.Agent.desiredVelocity.normalized + momentum, deltaTime);

            if (bossStateMachine.ForceReceiver.Movement.y <= 0 || bossStateMachine.Controller.velocity.y <= 0f)
            {
                if (IsInChanseRange())
                {
                    bossStateMachine.SwitchState(new BossChasingState(bossStateMachine));
                    return;
                }

                bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
            }
        }

        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
        FaceTarget();
    }




}
