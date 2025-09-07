using UnityEngine;

public class SystemUIManagers : MonoBehaviour
{
    [SerializeField] GameObject[] title;
    [SerializeField] GameObject[] content;

    GameObject current;

    void Start()
    {
        this.gameObject.SetActive(false);
        HideContent();
    }

    void HideContent()
    {
        foreach (GameObject item in content)
        {
            item.SetActive(false);
        }
    }




    GameObject ReturnGameObject(string name)
    {
        foreach (GameObject item in content)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    void ActiveObject(GameObject next)
    {
        if (current != null)
        {
            current.SetActive(false);
        }
        current = next;
        current.SetActive(true);
    }

    public void ActiveContent(string name)
    {
        ActiveObject(ReturnGameObject(name));
    }

    public void OnStats()
    {
        ActiveObject(content[0]);
    }

    public void OnGetSkill()
    {
        ActiveObject(content[1]);
    }

    public void OnWeapon()
    {
        ActiveObject(content[2]);
    }

    public void OnBrew()
    {
        ActiveObject(content[3]);
    }

    public void OnMedicines()
    {
        ActiveObject(content[4]);
    }

    public void OnSettings()
    {
        ActiveObject(content[5]);
    }
}
