using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

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

    [Header("Stat Content UI")]
    [SerializeField] List<AttributeData> statsValue;
    [SerializeField] List<GameObject> statsContentUI;

    [Header("Brew Content UI")]
    [SerializeField] GameObject brewContent;
    [SerializeField] GameObject brewImageElement;

    GameObject isActiveObject;
    public event Action ActionCountinue;
    public event Action Rest;
    public PlayerCombat playerCombat { get; set; }


    void Start()
    {
        UpdateSkillImage();
        SetUpStats();
        UpdateBrewContent();

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
            else
            {
                SkillsUI[i].color = Color.white;
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

    // For stats content UI

    public void SetUpStats()
    {
        foreach (var statUI in statsContentUI)
        {
            foreach (var statValue in statsValue)
            {
                if (statUI.name == statValue.name)
                {
                    TextMeshProUGUI text = statUI.GetComponentInChildren<TextMeshProUGUI>(true);
                    text.text = statValue.Value.ToString();
                    Image image = statUI.GetComponentInChildren<Image>(true);
                    image.sprite = statValue.Thumbnail;
                }
            }
        }
    }

    // For brew content UI
    public void UpdateBrewContent()
    {
        List<InventoriesSlot> playerInventories = PlayerInventory.Instance.inventoryObject.Contains;
        for (int i = 0; i < playerInventories.Count; i++)
        {
            if (playerInventories[i] == null || playerInventories[i].itemBase == null)
            {
                continue;
            }

            if (!playerInventories[i].itemBase.isExits)
            {
                continue;
            }

            GameObject img = Instantiate(brewImageElement);
            img.GetComponent<RectTransform>().transform.parent = brewContent.transform;
            Image imageChild = img.GetComponentInChildren<Image>(true);
            imageChild.sprite = PlayerInventory.Instance.inventoryObject.Contains[i].itemBase.Thumbnail;
            PlayerInventory.Instance.inventoryObject.Contains[i].itemBase.isExits = true;
        }
    }
}
