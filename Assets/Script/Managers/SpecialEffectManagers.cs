using System;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Created Instance");

    }

    public void UnlockEffect(string effectName)
    {
        var skill = specialEffectsDataList.Find(name => name.effectName == effectName);
        if (skill != null && !skill.unlocked)
        {
            skill.unlocked = true;
            OnActiveEffect?.Invoke();
        }
    }
}
