using System;
using UnityEngine.UI;
using UnityEngine;

public class UIManagers : Singleton<UIManagers>
{
    [Header("Main UI")]
    [SerializeField] GameObject UICheckpointInteraction;
    [SerializeField] GameObject UISystem;


    [Header("Skill Content UI")]
    [SerializeField] GameObject skills;
    [SerializeField] GameObject stregth;
    [SerializeField] Image WeaponInUseImage;
    [SerializeField] Image[] SkillsUI;
    // [SerializeField] WeaponUI weaponUI;
    GameObject isActiveObject;



    public event Action ActionCountinue;
    public event Action Rest;
    public PlayerCombat playerCombat { get; set; }
    

    void Start()
    {
        UpdateSkillImage();
    }

    void OnEnable()
    {
        playerCombat = FindFirstObjectByType<PlayerCombat>();
    }

    public void ActiveCheckpointUI(bool state)
    {
        UICheckpointInteraction.SetActive(state);
    }

    public void OnLeave()
    {
        ActionCountinue?.Invoke();
        ActiveCheckpointUI(false);
    }

    public void OnRest()
    {
        Rest?.Invoke();
        OnLeave();
    }

    public void OnSystem(string name)
    {
        UISystem.SetActive(true);
        SystemUIManagers systemUIManagers = UISystem.GetComponent<SystemUIManagers>();
        systemUIManagers.ActiveContent(name);
    }

    public void OnExitSystem()
    {
        UISystem.SetActive(false);
    }


    // For skill content UI
    void ActiveContent(GameObject _gameObject)
    {
        if (isActiveObject != null)
        {
            isActiveObject.SetActive(false);
        }
        _gameObject.SetActive(true);
        isActiveObject = _gameObject;
    }

    public void UpdateSkillImage()
    {
        for (int i = 0; i < SkillsUI.Length; i++)
        {
            SkillsUI[i].sprite = PlayerSingleton.Instance.weapon.SkillsOfWeapon[i].Sprite;
            if (!PlayerSingleton.Instance.weapon.SkillsOfWeapon[i].unlocked)
            {
                SkillsUI[i].color = Color.gray;
            }
        }
        WeaponInUseImage.sprite = PlayerSingleton.Instance.weapon.Thumbnail;
    }

    public void ActiveStregthPanel()
    {
        ActiveContent(skills);
        stregth.SetActive(false);
    }

    public void GetSkill(Image image)
    {
        foreach (SkillData skill in PlayerSingleton.Instance.weapon.SkillsOfWeapon)
        {
            if (skill.Sprite == image.sprite && !skill.unlocked)
            {
                if (PlayerSkill.Instance.UnlockSkill(skill))
                {
                    image.color = Color.white;
                }
            }
        }
    }
}
