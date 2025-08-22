using System.Collections;
using UnityEngine;

public class GrandStarfall : Ability
{
    public override string Name => "GrandStarfall";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, skillData.EffectObject.transform.rotation);
        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        SphereCollider sphereCollider = skill.GetComponentInChildren<SphereCollider>(true);
        sphereCollider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}


public class DarkBullet : Ability
{
    public override string Name => "DarkBullet";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        BossStateMachine bossStateMachine = caster.GetComponent<BossStateMachine>();

        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, bossStateMachine.Projectile.position, skillData.EffectObject.transform.rotation);

        Vector3 targetPoint = bossStateMachine.Player.transform.position + Vector3.up * 1.5f;
        Vector3 targetDirection = (targetPoint - skill.transform.position).normalized;

        Rigidbody bulletRb = skill.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = Vector3.zero;
        bulletRb.AddForce(targetDirection * bossStateMachine.BulletForce, ForceMode.Impulse);

        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();

        CapsuleCollider collider = skill.GetComponentInChildren<CapsuleCollider>(true);
        collider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class MagicalExplosion : Ability
{
    public override string Name => "MagicalExplosion";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position + Vector3.up * 0.5F, skillData.EffectObject.transform.rotation);

        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();

        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class DarkLightning : Ability
{
    public override string Name => "DarkLightning";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, skillData.EffectObject.transform.rotation);

        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class DarkStreak : Ability
{
    public override string Name => "DarkStreak";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, skillData.EffectObject.transform.rotation);
        skill.transform.parent = caster.transform;
        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        BoxCollider boxCollider = skill.GetComponentInChildren<BoxCollider>(true);
        boxCollider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}
