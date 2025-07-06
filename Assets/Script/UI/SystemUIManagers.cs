using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SystemUIManagers : MonoBehaviour
{
    [SerializeField] GameObject[] title;
    [SerializeField] GameObject[] content;

    GameObject current;

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

    public void OnGetSkill()
    {
        ActiveObject(content[0]);
    }

    public void OnCraft()
    {
        ActiveObject(content[1]);
    }

    public void OnBrew()
    {
        ActiveObject(content[2]);
    }

    public void OnMedicines()
    {
        ActiveObject(content[3]);
    }

    public void OnSettings()
    {
        ActiveObject(content[4]);
    }
}
