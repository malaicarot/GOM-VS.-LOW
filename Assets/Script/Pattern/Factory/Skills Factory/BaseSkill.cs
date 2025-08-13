using UnityEngine;

public abstract class Ability
{
    public abstract string Name { get; }
    public abstract void Proccess(SkillData skillData, GameObject caster, Transform spawn);

}
