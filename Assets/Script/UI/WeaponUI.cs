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

    public event Action OnGetMainWeapon; // Sự kiện khi đổi vũ khí, sẽ cập nhật UI

    public List<Image> mainWeaponSprite { get; set; }
    List<Image> secondaryWeaponSprite;

    void Start()
    {
        mainWeaponSprite = new List<Image>();
        secondaryWeaponSprite = new List<Image>();
        GetImageComponent(ScrollbarContentMain, mainWeaponSprite);
        GetImageComponent(ScrollbarContentSecondary, secondaryWeaponSprite);
        UpdateWeaponThumbnail(mainWeaponSprite);
        UpdateSecondaryWeaponThumbnail(secondaryWeaponSprite);
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

    void GetImageComponent(GameObject contentObject, List<Image> images)
    {
        foreach (Image img in contentObject.GetComponentsInChildren<Image>(true))
        {
            images.Add(img);
        }
    }

    public void UpdateWeaponThumbnail(List<Image> weaponListType)
    {
        OnGetMainWeapon?.Invoke();
        if (weaponListType == null) { return; }
        for (int i = 0; i < weaponListType.Count; i++)
        {
            weaponListType[i].sprite = PlayerSingleton.Instance.weaponSOList[i].Thumbnail;
        }
    }

    public void UpdateSecondaryWeaponThumbnail(List<Image> weaponListType)
    {
        if (weaponListType == null) { return; }
        for (int i = 0; i < weaponListType.Count; i++)
        {
            weaponListType[i].sprite = PlayerSingleton.Instance.weaponSOSecondaryList[i].Thumbnail;
        }
    }

    public void GetMainWeapon(Button button)
    {
        PlayerSkill.Instance.Reset();
        string mainWeaponName = button.GetComponent<Image>().sprite.name;
        PlayerSingleton.Instance.EquipWeapon(mainWeaponName, PlayerSingleton.Instance.weaponRight.transform);
        UIManagers.Instance.UpdateSkillImage();
    }

    public void GetSecondaryWeapon(Button button)
    {
        string secondaryWeaponName = button.GetComponent<Image>().sprite.name;
        PlayerSingleton.Instance.EquipWeapon(secondaryWeaponName, PlayerSingleton.Instance.weaponLeft.transform);
    }
}
