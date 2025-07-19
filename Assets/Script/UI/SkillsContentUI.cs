using UnityEngine;

public class SkillsContentUI : MonoBehaviour
{
    [SerializeField] GameObject skills;
    [SerializeField] GameObject stregth;
    [SerializeField] GameObject special;

    GameObject isActiveObject;

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

    public void ActiveSkillsPanel()
    {
        ActiveContent(skills);
    }

    public void ActiveStregthPanel()
    {
        ActiveContent(stregth);
    }

    public void ActiveSpecialPanel()
    {
        ActiveContent(special);
    }
}
