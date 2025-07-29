using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject weaponRight;
    [SerializeField] GameObject weaponLeft;
    public List<SpecialEffectsData> specialEffectsActiveList = new List<SpecialEffectsData>();
    public List<WeaponSO> weaponSOList;
    public List<WeaponSO> weaponSOSecondaryList;
    public event Action OnSetWeapon;
    public WeaponSO weapon { get; private set; }
    public WeaponSO weaponSecondary { get; private set; }

    void Start()
    {
        SetUpEffectActive();
        EquipWeapon("VikingSword");
        EquipSecondaryWeapon("Dagger");
        OnSetWeapon?.Invoke();
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
            string name = effect.EffectName;
            Effect effectOn = EffectFactory.GetEffect(name);
            if (effectOn == null)
            {
                continue;
            }
            // effect.ActiveAtion();
            effectOn.Proccess(effect, this.gameObject);
        }
    }

    void EquipWeapon(string name)
    {
        foreach (WeaponSO weaponSO in weaponSOList)
        {
            if (weaponSO.Name == name)
            {
                weapon = weaponSO;
                Instantiate(weapon.WeaponPrefab, weaponRight.transform);
            }
        }
    }

    void EquipSecondaryWeapon(string name)
    {
        foreach (WeaponSO weaponSO in weaponSOSecondaryList)
        {
            if (weaponSO.Name == name)
            {
                weaponSecondary = weaponSO;
                Instantiate(weaponSecondary.WeaponPrefab, weaponLeft.transform);
            }
        }
    }
}
