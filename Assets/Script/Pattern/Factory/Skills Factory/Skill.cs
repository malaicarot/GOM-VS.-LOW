using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        skill.Play();

    }
}

public class Stoning : Ability
{
    public override string Name => "Stoning";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        skill.Play();
    }
}
