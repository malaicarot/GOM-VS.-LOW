using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject weaponRight;
    [SerializeField] GameObject weaponLeft;
    // public List<SpecialEffectsData> specialEffectsActiveList = new List<SpecialEffectsData>();
    public List<WeaponSO> weaponSOList;
    public List<WeaponSO> weaponSOSecondaryList;
    public event Action OnSetWeapon;
    public WeaponSO weapon { get; private set; }
    public WeaponSO weaponSecondary { get; private set; }
    public GameObject mainWeapon { get; private set; }
    public GameObject subWeapon { get; private set; }

    void Awake()
    {
        
    }
    void Start()
    {
        // SetUpEffectActive();
        EquipWeapon("EarthHammer");
        EquipSecondaryWeapon("Iron_Dagger");
        OnSetWeapon?.Invoke();

        PlayerSkill.Instance.GetSkillDatas(weapon.SkillsOfWeapon);
        PlayerSkill.Instance.SetUp();
    }

    // void GetSkill()
    // {
        
    // }

    // void Update()
    // {
    //     ApplyEffect();
    // }

    // void OnEnable()
    // {
    //     SpecialEffectManagers.Instance.OnActiveEffect += SetUpEffectActive;
    // }

    // void SetUpEffectActive()
    // {
    //     foreach (SpecialEffectsData effect in SpecialEffectManagers.Instance.specialEffectsDataList)
    //     {
    //         if (effect.unlocked)
    //         {
    //             specialEffectsActiveList.Add(effect);
    //         }
    //     }
    // }


    // void ApplyEffect()
    // {
    //     foreach (SpecialEffectsData effect in specialEffectsActiveList)
    //     {
    //         string name = effect.EffectName;
    //         Effect effectOn = EffectFactory.GetEffect(name);
    //         if (effectOn == null)
    //         {
    //             continue;
    //         }
    //         // effect.ActiveAtion();
    //         effectOn.Proccess(effect, this.gameObject);
    //     }
    // }


    void GetSkillOfWeapon()
    {
        // foreach()
    }

    void EquipWeapon(string name)
    {
        foreach (WeaponSO weaponSO in weaponSOList)
        {
            if (weaponSO.Name == name)
            {
                if (mainWeapon != null)
                {
                    mainWeapon = null;
                }
                weapon = weaponSO;
                mainWeapon = Instantiate(weapon.WeaponPrefab, weaponRight.transform);
            }
        }
    }

    void EquipSecondaryWeapon(string name)
    {
        foreach (WeaponSO weaponSO in weaponSOSecondaryList)
        {
            if (weaponSO.Name == name)
            {
                if (subWeapon != null)
                {
                    subWeapon = null;
                }
                weaponSecondary = weaponSO;
                subWeapon = Instantiate(weaponSecondary.WeaponPrefab, weaponLeft.transform);
            }
        }
    }
}
