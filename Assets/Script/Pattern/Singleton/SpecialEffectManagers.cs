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
    PlayerStats playerStats;

    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        OnReadySingleton?.Invoke();
    }

    public bool UnlockEffect(SkillData skillData)
    {
        if (playerStats.skillUpPoint.Value == skillData.DragonVeinPoint)
        {
            playerStats.skillUpPoint.Value--;
            skillData.unlocked = true;
            return true;
        }
        return false;
    }
}
