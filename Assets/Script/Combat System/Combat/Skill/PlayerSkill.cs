using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject[] skillUI;
    [field: SerializeField] public SkillData[] skillData { get; private set; }

    Image backgroundImage; // Icon kĩ năng được làm mờ 
    Image fillImage;       // Icon kĩ năng sắc nét, dùng để làm hiệu ứng hồi chiêu
    List<Image> fillImageList;
    List<StatusBar> statusBarList;
    float test = 100f;
    const float FILL_CONST = 100f;

    void Start()
    {
        fillImageList = new List<Image>();
        statusBarList = new List<StatusBar>();
        UpdateImage();
        SetUpStatusBar();
    }
    void Update()
    {
        // FillUI();
    }

    void SetUpStatusBar()
    {
        foreach (var item in fillImageList)
        {
            StatusBar statusBar = item.gameObject.GetComponent<StatusBar>();
            statusBarList.Add(statusBar);
        }
    }

    void FillUI()
    {
        foreach (var item in fillImageList)
        {
            if (item.fillAmount < 1)
            {
                StatusBar statusBar = item.gameObject.GetComponent<StatusBar>();
                statusBar.fillAmount = 1f;
            }
        }
    }

    public void SetUpFill(string name)
    {
        foreach (var item in fillImageList)
        {
            if (item.sprite.name == name)
            {
                StatusBar statusBar = item.gameObject.GetComponent<StatusBar>();
                statusBar.fillAmount = 0 / FILL_CONST;
            }
        }
    }

    void UpdateImage()
    {
        if (skillData.Length <= 0) { return; }

        for (int i = 0; i < skillData.Length; i++)
        {
            backgroundImage = skillUI[i].transform.Find("Background")?.GetComponent<Image>();
            fillImage = skillUI[i].transform.Find("Fill")?.GetComponent<Image>();

            backgroundImage.sprite = skillData[i].sprite;
            fillImage.sprite = skillData[i].sprite;
            fillImage.fillAmount = 1;
            fillImageList.Add(fillImage);

            backgroundImage.gameObject.SetActive(true);
            fillImage.gameObject.SetActive(true);
        }
    }
}
