using UnityEngine;

public class Keep : Ability
{
    public override string Name => "Keep";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        Target target = playerStateMachine?.Targeter.currentTarget;
        if (target == null)
        {
            Debug.Log("No tager!");
            return;
        }

        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, target.transform.position, skillData.EffectObject.transform.rotation);

        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        SphereCollider sphereCollider = skill.GetComponentInChildren<SphereCollider>(true);
        sphereCollider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);

    }
}

public class Earthquake : Ability
{
    public override string Name => "Earthquake";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, spawn.transform.rotation);

        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        BoxCollider boxCollider = skill.GetComponentInChildren<BoxCollider>(true);
        boxCollider.enabled = true;
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class EarthEnhancement : Ability
{
    public override string Name => "EarthEnhancement";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        playerStateMachine.PlayerStats.ReturnValue(10);

        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, spawn.transform.rotation);
        skill.transform.SetParent(playerStateMachine.Enhancement);
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

