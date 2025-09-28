using UnityEngine;
using UnityEngine.UI;

public class SkillsContentUI : MonoBehaviour
{
    [SerializeField] GameObject skills;
    [SerializeField] GameObject stregth;
    [SerializeField] Image WeaponInUseImage;
    [SerializeField] Image[] SkillsUI;
    [SerializeField] WeaponUI weaponUI;

    GameObject isActiveObject;


    void Start()
    {
        weaponUI.OnGetMainWeapon += UpdateSkillImage;

    }

    void OnEnable()
    {
        ActiveStregthPanel();
    }



    void ActiveContent(GameObject _gameObject)
    {
        if (isActiveObject != null)
        {
            isActiveObject.SetActive(false);
        }
        _gameObject.SetActive(true);
        isActiveObject = _gameObject;
    }

    void UpdateSkillImage()
    {
        WeaponInUseImage.sprite = UIManagers.Instance.playerCombat.weapon.Thumbnail;
        for (int i = 0; i < SkillsUI.Length; i++)
        {
            SkillsUI[i].sprite = UIManagers.Instance.playerCombat.weapon.SkillsOfWeapon[i].Sprite;
            if (!UIManagers.Instance.playerCombat.weapon.SkillsOfWeapon[i].unlocked)
            {
                SkillsUI[i].color = Color.gray;
            }
        }
    }

    public void ActiveSkillsPanel()
    {
        ActiveContent(skills);
    }

    public void ActiveStregthPanel()
    {
        ActiveContent(skills);
        stregth.SetActive(false);

    }
}
