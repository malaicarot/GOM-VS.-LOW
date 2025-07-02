using System;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static UIManagers UIManager;

    [SerializeField] GameObject UICheckpointInteraction;
    public event Action StopAction;

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
        if (!state)
        {
            StopAction?.Invoke();
        }

        UICheckpointInteraction.SetActive(state);
    }
}
