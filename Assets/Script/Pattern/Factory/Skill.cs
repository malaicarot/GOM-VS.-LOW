using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.effect, caster.transform.position, Quaternion.identity);
        skill.Play();
        // caster.GetComponent<Animator>()?.SetTrigger(skillData.animation);
        
    }
}


public class Stoning : Ability
{
    public override string Name => "Stoning";

    public override void Proccess(SkillData skillData, GameObject caster)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.effect, caster.transform.position, Quaternion.identity);
        skill.Play();
        Debug.Log("Stoning");
    }
}
