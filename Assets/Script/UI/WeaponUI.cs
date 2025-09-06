using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] GameObject ScrollbarContentMain;
    [SerializeField] GameObject ScrollbarContentSecondary;


    List<Image> mainWeaponSprite;
    List<Image> secondaryWeaponSprite;

    void Start()
    {
        mainWeaponSprite = new List<Image>();
        secondaryWeaponSprite = new List<Image>();
        GetImageComponent(ScrollbarContentMain, mainWeaponSprite);
        GetImageComponent(ScrollbarContentSecondary, secondaryWeaponSprite);
        UpdateWeaponThumbnail(mainWeaponSprite);
    }


    void GetImageComponent(GameObject contentObject, List<Image> images)
    {
        foreach (Image img in contentObject.GetComponentsInChildren<Image>(true))
        {
            Debug.Log(img.name);
            images.Add(img);
        }
    }


    public void UpdateWeaponThumbnail(List<Image> weaponListType)
    {
        if (weaponListType == null) { return; }
        for (int i = 0; i < weaponListType.Count; i++)
        {
            weaponListType[i].sprite = UIManagers.Instance.playerCombat.weaponSOList[i].Thumbnail;
        }
    }


    public void GetWeapon(Button button)
    {
        PlayerSkill.Instance.Reset();
        string mainWeaponName = button.GetComponent<Image>().sprite.name;
        UIManagers.Instance.playerCombat.EquipWeapon(mainWeaponName);
    }
}
