using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    SkillData skillData;

    public void Init(SkillData _skillData)
    {
        skillData = _skillData;
        image.sprite = _skillData.Sprite;
        GetComponent<Button>().onClick.AddListener(OnClickSlot);
        Refesh();
    }

    void OnClickSlot()
    {
        if (!skillData.unlocked)
        {
            PlayerSkill.playerSkillSingleton.UnlockSkill(skillData.SkillName);
            Refesh();
        }
    }

    void Refesh()
    {
        image.color = skillData.unlocked ? Color.white : Color.gray;
    }
}
