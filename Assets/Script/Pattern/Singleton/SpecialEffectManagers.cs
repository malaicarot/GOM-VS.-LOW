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


    protected override void Awake()
    {
        base.Awake();
        OnReadySingleton?.Invoke();
    }
    // void Start()
    // {
    // }

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
