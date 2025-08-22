using UnityEngine;

public class BossApproachState : BossBaseState
{
    readonly int DashHash = Animator.StringToHash("Dash");

    string skillName = "DarkStreak";
    public BossApproachState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(DashHash, bossStateMachine.CrossFadeDuration);
        if (bossStateMachine.isPhaseTwo)
        {
            bossStateMachine.UseSkill(skillName, bossStateMachine.BossSkill.GetSkillBaseName(skillName), bossStateMachine.transform);
        }
    }

    public override void Tick(float deltaTime)
    {
        ApproachToPlayer(deltaTime);
        if (IsInAttackRange())
        {
            bossStateMachine.SwitchState(new BossAttackState(bossStateMachine, 0));
            return;
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

    void ApproachToPlayer(float deltaTime)
    {
        if (bossStateMachine.Agent != null && bossStateMachine.Agent.isOnNavMesh)
        {
            bossStateMachine.Agent.destination = bossStateMachine.Player.transform.position;
            Move(bossStateMachine.Agent.desiredVelocity.normalized * bossStateMachine.DashSpeed, deltaTime);
        }
        bossStateMachine.Agent.velocity = bossStateMachine.Controller.velocity;
    }
}
