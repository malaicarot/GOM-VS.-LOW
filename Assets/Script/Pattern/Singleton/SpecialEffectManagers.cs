using System;
using System.Collections.Generic;


public enum SpecialEffectType
{
    None,
    FirstHit,
    OnCritical,
    OnKill,
    OnCast,
    Custom
}
public class SpecialEffectManagers : Singleton<SpecialEffectManagers>
{
    public List<SpecialEffectsData> specialEffectsDataList;
    public event Action OnActiveEffect;
    public event Action OnReadySingleton;

    void Start()
    {
        OnReadySingleton?.Invoke();
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
