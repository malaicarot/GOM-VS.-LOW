using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] GameObject ScrollbarContentMain;
    [SerializeField] GameObject ScrollbarContentSecondary;
    [SerializeField] Image MainWeaponInUse;
    [SerializeField] Image SecondaryWeaponInUse;

    public event Action OnGetMainWeapon;

    public List<Image> mainWeaponSprite;
    List<Image> secondaryWeaponSprite;

    void Start()
    {
        // UIManagers.Instance.playerCombat.EquipWeapon("EarthHammer");
        // UIManagers.Instance.playerCombat.EquipSecondaryWeapon("Iron_Dagger");
        mainWeaponSprite = new List<Image>();
        secondaryWeaponSprite = new List<Image>();
        GetImageComponent(ScrollbarContentMain, mainWeaponSprite);
        GetImageComponent(ScrollbarContentSecondary, secondaryWeaponSprite);
        UpdateWeaponThumbnail(mainWeaponSprite);
        UpdateSecondaryWeaponThumbnail(secondaryWeaponSprite);
    }

    void OnEnable()
    {
        UIManagers.Instance.playerCombat.OnSetMainWeapon += SetMainWeaponInUse;
        UIManagers.Instance.playerCombat.OnSetSecondaryWeapon += SetSecondaryWeaponInUse;
    }

    void SetMainWeaponInUse()
    {
        MainWeaponInUse.sprite = UIManagers.Instance.playerCombat.weapon.Thumbnail;
    }

    void SetSecondaryWeaponInUse()
    {
        SecondaryWeaponInUse.sprite = UIManagers.Instance.playerCombat.weaponSecondary.Thumbnail;
    }


    void GetImageComponent(GameObject contentObject, List<Image> images)
    {
        foreach (Image img in contentObject.GetComponentsInChildren<Image>(true))
        {
            images.Add(img);
        }
    }


    public void UpdateWeaponThumbnail(List<Image> weaponListType)
    {
        if (weaponListType == null) { return; }
        OnGetMainWeapon?.Invoke();
        for (int i = 0; i < weaponListType.Count; i++)
        {
            weaponListType[i].sprite = UIManagers.Instance.playerCombat.weaponSOList[i].Thumbnail;
        }
    }

    public void UpdateSecondaryWeaponThumbnail(List<Image> weaponListType)
    {
        if (weaponListType == null) { return; }
        for (int i = 0; i < weaponListType.Count; i++)
        {
            weaponListType[i].sprite = UIManagers.Instance.playerCombat.weaponSOSecondaryList[i].Thumbnail;
        }
    }


    public void GetMainWeapon(Button button)
    {
        PlayerSkill.Instance.Reset();
        string mainWeaponName = button.GetComponent<Image>().sprite.name;
        UIManagers.Instance.playerCombat.EquipWeapon(mainWeaponName);
    }

    public void GetSecondaryWeapon(Button button)
    {
        string secondaryWeaponName = button.GetComponent<Image>().sprite.name;
        UIManagers.Instance.playerCombat.EquipSecondaryWeapon(secondaryWeaponName);
    }
}
