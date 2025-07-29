using UnityEngine;

public class PlayerCastSkillState : PlayerBaseState
{
    readonly int CastSkillHash = Animator.StringToHash("CastSkill");
    readonly string CastSkillTag = "CastSkill";
    readonly string overideName = "DefaultSkill";
    int skillIndex;
    Ability ability;


    public PlayerCastSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        skillIndex = stateMachine.InputReader.ButtonIndex;
        UseSkill(skillIndex);
        stateMachine.Mana.ReduceMana(PlayerSkill.playerSkillSingleton.alreadySkill[skillIndex].ManaCost);
        stateMachine.PlayAnimation(stateMachine.AnimatorOverrideController, overideName, CastSkillHash, PlayerSkill.playerSkillSingleton.alreadySkill[skillIndex].Animation);
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
        string name = PlayerSkill.playerSkillSingleton.alreadySkill[index].SkillName;
        Debug.Log(name);
        ability = AbilityFactory.GetAbility(name);
        if (ability != null)
        {
            ability.Proccess(PlayerSkill.playerSkillSingleton.alreadySkill[index], stateMachine.gameObject);
        }
    }
}
