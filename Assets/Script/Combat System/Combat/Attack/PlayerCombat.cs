using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject weaponRight;
    [SerializeField] GameObject weaponLeft;
    public List<WeaponSO> weaponSOList;
    public List<WeaponSO> weaponSOSecondaryList;
    public event Action OnSetWeapon;
    public WeaponSO weapon { get; private set; }
    public WeaponSO weaponSecondary { get; private set; }
    public GameObject mainWeapon { get; private set; }
    public GameObject subWeapon { get; private set; }

    Material weaponMaterial;


    void Start()
    {
        EquipWeapon("EarthHammer");
        EquipSecondaryWeapon("Iron_Dagger");
        OnSetWeapon?.Invoke();

        PlayerSkill.Instance.GetSkillDatas(weapon.SkillsOfWeapon);
        PlayerSkill.Instance.SetUp();
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
