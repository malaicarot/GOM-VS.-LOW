using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectManagers : MonoBehaviour
{
    public static SpecialEffectManagers specialEffectManagers;
    public List<SpecialEffectsData> specialEffectsDataList;


    void Awake()
    {
        if (specialEffectManagers != null)
        {
            Destroy(specialEffectManagers);
        }
        else
        {
            specialEffectManagers = this;
        }
    }

    public void UnlockEffect(string effectName)
    {
        var skill = specialEffectsDataList.Find(name => name.effectName == effectName);
        if (skill != null && !skill.unlocked)
        {
            skill.unlocked = true;
        }
    }

    public bool ApplyFirstHit()
    {
        foreach (var effect in specialEffectsDataList)
        {
            if (effect.unlocked && effect.trigger == SpecialEffectsData.TriggerType.OnFirstHit)
            {
                return true;
            }
        }
        return false;
    }

    public AnimationClip FirstHitAnimation()
    {
        foreach (var effect in specialEffectsDataList)
        {
            if (effect.unlocked && effect.trigger == SpecialEffectsData.TriggerType.OnFirstHit)
            {
                return effect.Animation;
            }
        }
        return null;
    }
}
