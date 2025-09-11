using UnityEngine;

public class LevelUp : Effect
{
    public override string Name => "LevelUp";

    public override void Proccess(GameObject caster)
    {
        EffectPool.EffectPoolSingleton.GetEffect(Name, caster.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySFX("LevelUp");
    }
}
