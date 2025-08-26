using UnityEngine;

public class PlayerCastSkillState : PlayerBaseState
{
    readonly string CastSkillTag = "Skill";
    int skillIndex;
    Ability ability;


    public PlayerCastSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        skillIndex = stateMachine.InputReader.ButtonIndex;
        Debug.Log(skillIndex);
        UseSkill(skillIndex);
        stateMachine.Mana.ReduceMana(PlayerSkill.Instance.alreadySkill[skillIndex].ManaCost);
        stateMachine.Animator.CrossFadeInFixedTime(PlayerSkill.Instance.alreadySkill[skillIndex].AnimationName, stateMachine.CrossFadeDuration);
        PlayerSkill.Instance.TargetIndentify(stateMachine.Targeter.currentTarget);
    }

    public override void Tick(float deltaTime)
    {

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, CastSkillTag);
        if (normalizedTime >= 0.8f && normalizedTime <= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
    }

    void UseSkill(int index)
    {
        string name = PlayerSkill.Instance.alreadySkill[index].SkillName;
        
        ability = AbilityFactory.GetAbility(name);
        if (ability != null)
        {
            ability.Proccess(PlayerSkill.Instance.alreadySkill[index], stateMachine.gameObject, stateMachine.SkillPosition.transform);
        }
    }
}
