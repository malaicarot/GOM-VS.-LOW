using UnityEngine;

public class GrandStarfall : Ability
{
    public override string Name => "GrandStarfall";

    public override void Proccess(SkillData skillData, GameObject caster)
    {
        ParticleSystem skill = GameObject.Instantiate(skillData.Effect, caster.transform.position, skillData.Effect.transform.rotation);
        AttackDealDamage attackDealDamage = skill.GetComponent<AttackDealDamage>();
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        SphereCollider sphereCollider = skill.GetComponent<SphereCollider>();
        sphereCollider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
        Debug.Log(skillData.Damage);
        skill.Play();
    }
}
