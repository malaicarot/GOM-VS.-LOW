using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] GameObject ScrollbarContentMain;
    [SerializeField] GameObject ScrollbarContentSecondary;
    [SerializeField] GameObject WeaponButtonPrefab;
    [SerializeField] Image MainWeaponInUse;
    [SerializeField] Image SecondaryWeaponInUse;

    public event Action OnGetMainWeapon; // Sự kiện khi đổi vũ khí, sẽ cập nhật UI

    // public List<Image> mainWeaponSprite { get; set; }


    List<Button> main;
    List<Button> sub;


    void Start()
    {
        main = new List<Button>();
        sub = new List<Button>();

        InstantiateButton(PlayerSingleton.Instance.weaponSOList, ScrollbarContentMain, true);
        InstantiateButton(PlayerSingleton.Instance.weaponSOSecondaryList, ScrollbarContentSecondary, false);
    }

    void OnEnable()
    {
        PlayerSingleton.Instance.OnSetMainWeapon += SetMainWeaponInUse;
        PlayerSingleton.Instance.OnSetSecondaryWeapon += SetSecondaryWeaponInUse;
    }

    void SetMainWeaponInUse()
    {
        MainWeaponInUse.sprite = PlayerSingleton.Instance.weapon.Thumbnail;
    }

    void SetSecondaryWeaponInUse()
    {
        SecondaryWeaponInUse.sprite = PlayerSingleton.Instance.weaponSecondary.Thumbnail;
    }

    void InstantiateButton(List<WeaponSO> listWeapons, GameObject parent, bool isMainWeapon)
    {
        foreach (var weapon in listWeapons)
        {
            GameObject weaponButton = Instantiate(WeaponButtonPrefab);
            Button button = weaponButton.GetComponent<Button>();
            Image childImage = weaponButton.transform.GetChild(0).GetComponent<Image>();
            childImage.sprite = weapon.Thumbnail;
            weaponButton.GetComponent<RectTransform>().transform.SetParent(parent.transform);

            if (isMainWeapon)
            {
                main.Add(button);
                button.onClick.AddListener(() => GetMainWeapon(weapon.Name));

            }
            else
            {
                sub.Add(button);
                button.onClick.AddListener(() => GetSecondaryWeapon(weapon.Name));
            }
        }
    }

    public void GetMainWeapon(string weaponName)
    {
        PlayerSkill.Instance.Reset();
        PlayerSingleton.Instance.EquipWeapon(weaponName, PlayerSingleton.Instance.weaponRight.transform);
        UIManagers.Instance.UpdateSkillImage();
    }

    public void GetSecondaryWeapon(string weaponName)
    {
        PlayerSingleton.Instance.EquipSecondaryWeapon(weaponName, PlayerSingleton.Instance.weaponLeft.transform);
    }
}
