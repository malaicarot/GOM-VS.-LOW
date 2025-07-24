using UnityEngine;

public class SetUpSkillPanel : MonoBehaviour
{
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject prefabs;


    void OnEnable()
    {
        foreach (var skill in PlayerSkill.playerSkillSingleton.skillData)
        {
            var slotPrefab = Instantiate(prefabs, slotParent);
            RectTransform rectTransform = slotPrefab.GetComponent<RectTransform>();
            float XRecTransform = rectTransform.anchoredPosition.x + 238;
            rectTransform.anchoredPosition = new Vector2(XRecTransform, rectTransform.anchoredPosition.y);
            SkillsUI skillsUI = slotPrefab.GetComponent<SkillsUI>();
            // XRecTransform += 238;
            skillsUI.Init(skill);
        }
    }
}
