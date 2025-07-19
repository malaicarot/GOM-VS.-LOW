using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public event Action OnAttack;
    public event Action OnFirstHit;
    public event Action OnCritical;


    public void PerformAttack()
    {
        if (SpecialEffectManagers.specialEffectManagers.ApplyFirstHit())
        {
            Debug.Log("Ban su kien");
            OnFirstHit?.Invoke();
        }
    }
}
