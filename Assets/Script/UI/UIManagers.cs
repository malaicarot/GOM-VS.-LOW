using System;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static UIManagers UIManager;

    [SerializeField] GameObject UICheckpointInteraction;
    public event Action ActionCountinue;
    public event Action Rest;

    void Start()
    {
        if (UIManager != null)
        {
            Destroy(UIManager);
        }
        else
        {
            UIManager = this;
        }
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
}
