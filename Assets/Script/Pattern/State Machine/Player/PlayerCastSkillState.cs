using UnityEngine;

public class PlayerCastSkillState : PlayerBaseState
{
    readonly int CastSkillHash = Animator.StringToHash("CastSkill");
    readonly string CastSkillTag = "CastSkill";
    int skillIndex;
    Ability ability;


    public PlayerCastSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        skillIndex = stateMachine.InputReader.ButtonIndex;
        UseSkill(skillIndex);
        stateMachine.Mana.ReduceMana(stateMachine.PlayerSkill.skillData[skillIndex].manaCost);
        PlayAnimation(skillIndex);
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

    void UseSkill(int index)
    {
        string name = stateMachine.PlayerSkill.skillData[index].skillName;
        ability = AbilityFactory.GetAbility(name);
        if (ability != null)
        {
            ability.Proccess(stateMachine.PlayerSkill.skillData[index], stateMachine.gameObject);
        }
    }

    void PlayAnimation(int index)
    {
        AnimatorOverrideController runtimeOverride = new AnimatorOverrideController(stateMachine.AnimatorOverrideController);
        runtimeOverride["DefaultSkill"] = stateMachine.PlayerSkill.skillData[index].animation;
        stateMachine.Animator.runtimeAnimatorController = runtimeOverride;
        stateMachine.Animator.CrossFadeInFixedTime(CastSkillHash, stateMachine.CrossFadeDuration);
    }
}
