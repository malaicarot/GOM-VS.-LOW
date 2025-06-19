using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] Button[] buttonSkill;
    [SerializeField] SkillData[] skillData;

    [SerializeField] float cooldownTime;
    Image backgroundImage; // Icon kĩ năng được làm mờ 
    Image fillImage;       // Icon kĩ năng sắc nét, dùng để làm hiệu ứng hồi chiêu
    List<Image> fillImageList;
    Ability ability;



    void Start()
    {
        fillImageList = new List<Image>();
        UpdateImage();
    }

    void Update()
    {
        // CooldownUI();
    }

    public void UseSkill(int index)
    {
        if (index >= 0 && index < buttonSkill.Length)
        {
            buttonSkill[index].onClick.Invoke();
            ActiveEffect(index);
        }
    }

    void CooldownUI()
    {
        // for (int i = 0; i < fillImageList.Count; i++)
        // {
        //     fillImageList[i].fillAmount = Mathf.Lerp(0, 1, skilldata[i].coolDown);

        // }
    }


    void ActiveEffect(int index)
    {
        ability = AbilityFactory.GetAbility(skillData[index].skillName);
        if (ability != null)
        {
            ability.Proccess(skillData[index], this.transform);
            fillImageList[index].fillAmount = 0;
        }
    }


    void UpdateImage()
    {
        if (skillData.Length <= 0) { return; }

        for (int i = 0; i < skillData.Length; i++)
        {
            backgroundImage = buttonSkill[i].transform.Find("Background")?.GetComponent<Image>();
            fillImage = buttonSkill[i].transform.Find("Fill")?.GetComponent<Image>();

            backgroundImage.sprite = skillData[i].sprite;
            fillImage.sprite = skillData[i].sprite;
            fillImage.fillAmount = 1;
            fillImageList.Add(fillImage);

            backgroundImage.gameObject.SetActive(true);
            fillImage.gameObject.SetActive(true);
        }
    }

    public void ActiveEffect(ParticleSystem particleSystem, Transform effectTransform)
    {
        ParticleSystem tempParticle = Instantiate(particleSystem, effectTransform.position, Quaternion.identity);
        tempParticle.Play();
    }

}
