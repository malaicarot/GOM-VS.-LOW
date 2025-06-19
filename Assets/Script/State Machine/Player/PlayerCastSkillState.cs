using UnityEngine;

public class PlayerCastSkillState : PlayerBaseState
{
    readonly int CastSkillHash = Animator.StringToHash("Cast_Skill");
    readonly string CastSkillTag = "CastSkill";

    public PlayerCastSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(CastSkillHash, stateMachine.CrossFadeDuration);
        stateMachine.PlayerSkill.UseSkill(stateMachine.InputReader.ButtonIndex);
        stateMachine.Mana.ReduceMana(stateMachine.PlayerSkill.ManaCost(stateMachine.InputReader.ButtonIndex));
    }

    public override void Tick(float deltaTime)
    {

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, CastSkillTag);
        if (normalizedTime > 1f)
        {
            if (stateMachine.Targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }

    public override void Exit()
    {
    }
}
