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
        stateMachine.Mana.ReduceMana(PlayerSkill.playerSkillSingleton.skillData[skillIndex].ManaCost);
        stateMachine.PlayAnimation(CastSkillHash, PlayerSkill.playerSkillSingleton.skillData[skillIndex].Animation);
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
        string name = PlayerSkill.playerSkillSingleton.skillData[index].SkillName;
        ability = AbilityFactory.GetAbility(name);
        if (ability != null)
        {
            ability.Proccess(PlayerSkill.playerSkillSingleton.skillData[index], stateMachine.gameObject);
        }
    }
}
