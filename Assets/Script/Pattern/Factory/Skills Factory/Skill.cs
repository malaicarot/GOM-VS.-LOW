using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.Effect, caster.transform.position, caster.transform.rotation);
        skill.Play();

    }
}

public class Stoning : Ability
{
    public override string Name => "Stoning";

    public override void Proccess(SkillData skillData, GameObject caster)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.Effect, caster.transform.position, caster.transform.rotation);
        skill.Play();
    }
}
