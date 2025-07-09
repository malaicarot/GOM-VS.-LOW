using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject[] skillUI;
    [field: SerializeField] public SkillData[] skillData { get; private set; }

    Image backgroundImage; // Icon kĩ năng được làm mờ 
    Image fillImage;       // Icon kĩ năng sắc nét, dùng để làm hiệu ứng hồi chiêu
    List<Image> fillImageList;

    void Start()
    {
        fillImageList = new List<Image>();
        UpdateImage();
        SetCooldownSkill();
    }

    void UpdateImage()
    {
        if (skillData.Length <= 0) { return; }

        for (int i = 0; i < skillData.Length; i++)
        {
            backgroundImage = skillUI[i].transform.Find("Background")?.GetComponent<Image>();
            fillImage = skillUI[i].transform.Find("Fill")?.GetComponent<Image>();

            backgroundImage.sprite = skillData[i].Sprite;
            fillImage.sprite = skillData[i].Sprite;
            fillImage.fillAmount = 1;
            fillImageList.Add(fillImage);

            backgroundImage.gameObject.SetActive(true);
            fillImage.gameObject.SetActive(true);
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

    void SetCooldownSkill()
    {
        for (int i = 0; i < fillImageList.Count; i++)
        {
            CoolDown coolDown = fillImageList[i].GetComponent<CoolDown>();
            coolDown.coolDownTime = skillData[i].CoolDown;
        }
    }
}
