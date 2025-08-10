using System;
using UnityEngine;

public class UtilityAIManager : MonoBehaviour
{
    [SerializeField] float timeResetInterruped;
    public static UtilityAIManager UtilityAIManagerSingleton;
    public event Action OnInterrupedAction;
    public bool interruped { get; set; } = false;

    float countTimeReset;



    void Awake()
    {
        if (UtilityAIManagerSingleton == null)
        {
            UtilityAIManagerSingleton = this;
            DontDestroyOnLoad(UtilityAIManagerSingleton);
        }
        else
        {
            Destroy(UtilityAIManagerSingleton);
            return;
        }
    }



    void Update()
    {
        ResetInterrupped();
    }


    void ResetInterrupped()
    {
        Debug.Log(interruped);
        if (interruped)
        {
            countTimeReset += Time.deltaTime;
            if (countTimeReset >= timeResetInterruped)
            {
                interruped = false;
                countTimeReset = 0;
            }
        }
    }
}
