using Unity.VisualScripting;
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
        playerStateMachine.PlayerStats.ReturnResistanceValue(10);

        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, spawn.transform.rotation);
        skill.transform.SetParent(playerStateMachine.Enhancement);
    }
}

public class EarthDragon : Ability
{
    public override string Name => "EarthDragon";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, spawn.transform.rotation);
        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.gameObject.SetActive(true);
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}


public class Sear : Ability
{
    public override string Name => "Sear";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        Target target = playerStateMachine?.Targeter.currentTarget;
        if (target == null)
        {
            Debug.Log("No tager!");
            return;
        }
        PooledObject skill = EffectPool.EffectPoolSingleton.GetEffect(skillData.SkillName, spawn.position, skillData.EffectObject.transform.rotation);
        AttackDealDamage attackDealDamage = skill.GetComponentInChildren<AttackDealDamage>(true);
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class Conflagration : Ability
{
    public override string Name => "Conflagration";

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
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}

public class FireEnhancment : Ability
{
    public override string Name => "FireEnhancment";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        PlayerCombat playerCombat = playerStateMachine.PlayerCombat;
        playerCombat.Enhancment(191, 7, 0, 10);
    }
}



public class Reflective : Ability
{
    public override string Name => "Reflective";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        playerStateMachine.Health.SetParry(true);
    }
}

public class SolarEclipse : Ability
{
    public override string Name => "SolarEclipse";

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
        attackDealDamage.myCollider = caster.GetComponent<CharacterController>();
        attackDealDamage.SetAttack(skillData.Damage, skillData.KnockBack);
    }
}


public class LightEnhancment : Ability
{
    public override string Name => "LightEnhancment";

    public override void Proccess(SkillData skillData, GameObject caster, Transform spawn)
    {
        PlayerStateMachine playerStateMachine = caster.GetComponent<PlayerStateMachine>();
        // PlayerCombat playerCombat = playerStateMachine.PlayerCombat;
        // playerCombat.Enhancment(191, 7, 0, 10);


        playerStateMachine.PlayerStats.ReturnStaminaValue(100);
    }
}
