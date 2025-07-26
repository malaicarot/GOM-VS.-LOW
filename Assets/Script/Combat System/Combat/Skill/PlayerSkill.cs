using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill playerSkillSingleton;
    public event Action OnActiveSkill;
    [SerializeField] GameObject[] skillUI;
    [field: SerializeField] public List<SkillData> skillData { get; private set; }

    public List<SkillData> alreadySkill { get; private set; }


    Image backgroundImage; // Icon kĩ năng được làm mờ 
    Image fillImage;       // Icon kĩ năng sắc nét, dùng để làm hiệu ứng hồi chiêu
    List<Image> fillImageList;

    void Start()
    {
        if (playerSkillSingleton == null)
        {
            playerSkillSingleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
        fillImageList = new List<Image>();
        alreadySkill = new List<SkillData>();
        // UpdateImage();
        SetupStart();
        SetCooldownSkill();
    }




    void SetupStart()
    {
        foreach (SkillData skill in skillData)
        {
            if (skill.unlocked)
            {
                UpdateImage(skill);
            }
        }
    }

    public void UpdateImage(SkillData skillData)
    {

        for (int i = 0; i < skillUI.Length; i++)
        {
            backgroundImage = skillUI[i].transform.Find("Background")?.GetComponent<Image>();
            fillImage = skillUI[i].transform.Find("Fill")?.GetComponent<Image>();
            if (backgroundImage.sprite != null)
            {
                continue;
            }

            if (backgroundImage.sprite == null)
            {
                backgroundImage.sprite = skillData.Sprite;
                fillImage.sprite = skillData.Sprite;
                fillImage.fillAmount = 1;
                fillImageList.Add(fillImage);
                backgroundImage.gameObject.SetActive(true);
                fillImage.gameObject.SetActive(true);
                alreadySkill.Add(skillData);
                Debug.Log(alreadySkill[0]);
                break;
            }
        }
    }

    public bool ButtonOnClick(Button button)
    {
        CoolDown coolDown = button.GetComponentInChildren<CoolDown>();

        if (coolDown.cooldown < coolDown.coolDownTime)
        {
            return false;
        }
        return true;
    }

    public void UnlockSkill(string skillName)
    {
        SkillData skill = skillData.Find(name => name.SkillName == skillName);
        if (skill != null && !skill.unlocked)
        {
            skill.unlocked = true;
            UpdateImage(skill);
            OnActiveSkill?.Invoke();
        }
    }

    void SetCooldownSkill()
    {
        for (int i = 0; i < fillImageList.Count; i++)
        {
            CoolDown coolDown = fillImageList[i].GetComponent<CoolDown>();
            coolDown.coolDownTime = skillData[i].CoolDown;
        }
    }
}
