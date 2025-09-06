using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : Singleton<PlayerSkill>
{
    public event Action OnActiveSkill;
    [SerializeField] GameObject[] skillUI;
    public List<SkillData> skillDatas { get; set; }
    public List<SkillData> alreadySkill { get; set; }

    public event Action OnGotSkills;

    public Target target { get; private set; }


    Image backgroundImage; // Icon kĩ năng được làm mờ 
    Image fillImage;       // Icon kĩ năng sắc nét, dùng để làm hiệu ứng hồi chiêu
    List<Image> fillImageList;

    protected override void Awake()
    {
        base.Awake();
        Reset();
    }

    public void GetSkillDatas(List<SkillData> _skillDatas)
    {
        skillDatas = _skillDatas;
        Debug.Log(skillDatas);
    }

    public void SetUp()
    {
        SetupStart();
        SetCooldownSkill();
    }
    public void Reset()
    {
        fillImageList = new List<Image>();
        alreadySkill = new List<SkillData>();
    }

    void SetupStart()
    {
        for (int i = 0; i < skillDatas.Count; i++)
        {
            // if (skillDatas[i].unlocked)
            // {
                UpdateImage(skillDatas[i], i);
            // }
        }
        // foreach (SkillData skill in skillDatas)
        // {
        //     if (skill.unlocked)
        //     {
        //         UpdateImage(skill);
        //     }
        // }
    }

    public void UpdateImage(SkillData skillData, int index)
    {

        // for (int i = 0; i < skillUI.Length; i++)
        // {
            backgroundImage = skillUI[index].transform.Find("Background")?.GetComponent<Image>();
            fillImage = skillUI[index].transform.Find("Fill")?.GetComponent<Image>();
            // if (backgroundImage.sprite != null)
            // {
                // continue;
            // }

            // if (backgroundImage.sprite == null)
            // {
                backgroundImage.sprite = skillData.Sprite;
                fillImage.sprite = skillData.Sprite;
                fillImage.fillAmount = 1;
                fillImageList.Add(fillImage);
                backgroundImage.gameObject.SetActive(true);
                fillImage.gameObject.SetActive(true);
                alreadySkill.Add(skillData);
            //     break;
            // }
        // }
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
        SkillData skill = skillDatas.Find(name => name.SkillName == skillName);
        if (skill != null && !skill.unlocked)
        {
            skill.unlocked = true;
            // UpdateImage(skill);
            OnActiveSkill?.Invoke();
        }
    }

    void SetCooldownSkill()
    {
        for (int i = 0; i < fillImageList.Count; i++)
        {
            CoolDown coolDown = fillImageList[i].GetComponent<CoolDown>();
            coolDown.coolDownTime = skillDatas[i].CoolDown;
        }
    }


    public void TargetIndentify(Target _target)
    {
        if (_target == null)
        {
            return;
        }
        this.target = _target;
    }
}
