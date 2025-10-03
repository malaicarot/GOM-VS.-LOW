using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    public List<WeaponSO> weaponSOList;
    public List<WeaponSO> weaponSOSecondaryList;
    public event Action OnSetMainWeapon;
    public event Action OnSetSecondaryWeapon;

    public WeaponSO weapon { get; set; }
    public WeaponSO weaponSecondary { get; set; }
    public GameObject mainWeapon { get; private set; }
    public GameObject subWeapon { get; private set; }
    public GameObject weaponRight;
    public GameObject weaponLeft;
    Material weaponMaterial;


    void Start()
    {
        EquipWeapon("EarthHammer", weaponRight.transform);
        EquipSecondaryWeapon("Iron_Dagger", weaponLeft.transform);
    }

    public void PlaySFXWeaponHit()
    {
        SoundManager.Instance.PlaySFX(weapon.WeaponImpact.name);
    }

    public void EquipWeapon(string name, Transform weaponTransform)
    {
        foreach (WeaponSO weaponSO in weaponSOList)
        {
            if (weaponSO.Name == name)
            {
                if (mainWeapon != null)
                {
                    Destroy(mainWeapon);
                    weapon = null;
                }

                weapon = weaponSO;
                mainWeapon = Instantiate(weapon.WeaponPrefab, weaponTransform);
            }
        }

        PlayerSkill.Instance.GetSkillDatas(weapon.SkillsOfWeapon);
        PlayerSkill.Instance.SetUp();
        UIManagers.Instance.UpdateSkillImage();
        OnSetMainWeapon?.Invoke();
    }

    public void EquipSecondaryWeapon(string name, Transform weaponTransform)
    {
        foreach (WeaponSO weaponSO in weaponSOSecondaryList)
        {
            if (weaponSO.Name == name)
            {
                if (subWeapon != null)
                {
                    Destroy(subWeapon);
                    subWeapon = null;
                }
                weaponSecondary = weaponSO;
                subWeapon = Instantiate(weaponSecondary.WeaponPrefab, weaponTransform);
            }
        }
        OnSetSecondaryWeapon?.Invoke();
    }


    public void Enhancment(int R, int B, int G, int damage)
    {
        StartCoroutine(ChangeMaterial(R, B, G, damage));
    }

    public IEnumerator ChangeMaterial(int R, int B, int G, int damage)
    {
        MeshRenderer meshRenderer = mainWeapon.GetComponent<MeshRenderer>();
        weaponMaterial = meshRenderer.material;
        Color baseColor = new Color(R, G, B);
        Color emissiveColor = baseColor * 10;
        weaponMaterial.SetColor("_EmissiveColor", emissiveColor);
        DamageUp(damage);
        // weaponMaterial.SetFloat("_ExposureWeight", 0.8f);

        yield return new WaitForSecondsRealtime(7f);
        Color emissiveBaseColor = baseColor * 1;
        weaponMaterial.SetColor("_EmissiveColor", emissiveBaseColor);
        DamageUp(-damage);
    }

    public void DamageUp(int damage)
    {
        foreach (var attackDamage in weapon.Attacks)
        {
            attackDamage.AttackDamage += damage;
        }
    }

}
