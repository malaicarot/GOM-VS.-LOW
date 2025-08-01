using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialEffectType
{
    None,
    FirstHit,
    OnCritical,
    OnKill,
    OnCast,
    Custom
}
public class SpecialEffectManagers : MonoBehaviour
{
    public static SpecialEffectManagers specialEffectManagers;
    public List<SpecialEffectsData> specialEffectsDataList;



    public event Action OnActiveEffect;
    public event Action OnReadySingleton;

    void Awake()
    {
        if (specialEffectManagers != null && specialEffectManagers != this)
        {
            Destroy(specialEffectManagers);
            return;
        }
        else
        {
            specialEffectManagers = this;
            DontDestroyOnLoad(gameObject);
            OnReadySingleton?.Invoke();
        }
    }

    public void UnlockEffect(string effectName)
    {
        var skill = specialEffectsDataList.Find(name => name.EffectName == effectName);
        if (skill != null && !skill.unlocked)
        {
            skill.unlocked = true;
            OnActiveEffect?.Invoke();
        }
    }
}
