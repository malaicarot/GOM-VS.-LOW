using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        // GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        // ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        // skill.Play();
        Debug.Log(Name);

    }
}

public class Earthquake : Ability
{
    public override string Name => "Earthquake";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        // GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        // ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        // skill.Play();
        Debug.Log(Name);
    }
}

public class EarthEnhancement : Ability
{
    public override string Name => "Enhancement";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        // GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        // ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        // skill.Play();
        Debug.Log(Name);
    }
}

public class Meteorite : Ability
{
    public override string Name => "Meteorite";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        // GameObject gameObject = GameObject.Instantiate(skillData.EffectObject, spawn.position, spawn.rotation);
        // ParticleSystem skill = gameObject.GetComponent<ParticleSystem>();
        // skill.Play();
        Debug.Log(Name);
    }
}
