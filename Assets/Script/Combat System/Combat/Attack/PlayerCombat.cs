using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<SpecialEffectsData> specialEffectsActiveList = new List<SpecialEffectsData>();

    void Start()
    {
        SetUpEffectActive();
    }


    void OnEnable()
    {
        SpecialEffectManagers.specialEffectManagers.OnActiveEffect += SetUpEffectActive;
    }

    void OnDisable()
    {
        SpecialEffectManagers.specialEffectManagers.OnActiveEffect -= SetUpEffectActive;
    }

    void Update()
    {
        ApplyEffect();
    }

    void SetUpEffectActive()
    {
        foreach (SpecialEffectsData effect in SpecialEffectManagers.specialEffectManagers.specialEffectsDataList)
        {
            if (effect.unlocked)
            {
                specialEffectsActiveList.Add(effect);
            }
        }
    }


    void ApplyEffect()
    {
        foreach (SpecialEffectsData effect in specialEffectsActiveList)
        {
            string name = effect.effectName;
            Effect effectOn = EffectFactory.GetEffect(name);
            if (effectOn == null)
            {
                continue;
            }
            // effect.ActiveAtion();
            effectOn.Proccess(effect, this.gameObject);
        }
    }
}
