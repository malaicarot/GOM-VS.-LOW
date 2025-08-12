
using UnityEngine;

public class BossJumpAttackSate : BossBaseState
{
    readonly int JumpHash = Animator.StringToHash("Jump");
    Vector3 momentum;
    string skillName = "GrandStarfall";
    float timeToDelay = 2f;
    bool isOneTime = false;
    public BossJumpAttackSate(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {

    }

    public override void Enter()
    {
        bossStateMachine.countJumpAttack++;
        bossStateMachine.Animator.CrossFadeInFixedTime(JumpHash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.ForceReceiver.AddJumpForce(bossStateMachine.BossJumpForce);
        momentum = bossStateMachine.Agent.velocity;
        momentum.y = 0f;
    }

    public override void Tick(float deltaTime)
    {
        AccumulateAttack(deltaTime);
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

    void AccumulateAttack(float deltaTime)
    {
        if (bossStateMachine.Agent != null)
        {
            bossStateMachine.Agent.destination = bossStateMachine.Player.transform.position;
            Move(bossStateMachine.Agent.desiredVelocity.normalized + momentum, deltaTime);
            bossStateMachine.ForceReceiver.AddMultiplierForFall(bossStateMachine.FallMultiplier);
            if (bossStateMachine.Controller.isGrounded)
            {
                if (!isOneTime)
                {
                    bossStateMachine.UseSkill(skillName, bossStateMachine.BossSkill.GetSkillBaseName(skillName));
                    isOneTime = true;
                }

                if (bossStateMachine.countJumpAttack < bossStateMachine.JumpAttackTime)
                {
                    timeToDelay -= deltaTime;
                    if (timeToDelay <= 0)
                    {
                        bossStateMachine.SwitchState(new BossJumpAttackSate(bossStateMachine));
                        return;
                    }
                }
                else
                {
                    bossStateMachine.countJumpAttack = 0;
                    bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
                    return;
                }
            }
        }
        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
    }
}
