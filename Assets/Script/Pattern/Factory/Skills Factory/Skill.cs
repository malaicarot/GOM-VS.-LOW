using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.Effect, spawn.position, spawn.rotation);
        skill.Play();

    }
}

public class Stoning : Ability
{
    public override string Name => "Stoning";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.Effect, spawn.position, spawn.rotation);
        skill.Play();
    }
}
