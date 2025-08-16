using System;
using UnityEngine;

public class UIManagers : Singleton<UIManagers>
{
    [SerializeField] GameObject UICheckpointInteraction;
    [SerializeField] GameObject UISystem;


    public event Action ActionCountinue;
    public event Action Rest;


    // void Start()
    // {
    //     Debug.Log("asdad");
    // }
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
}
