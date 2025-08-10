using System;
using UnityEngine;

public class UtilityAIManager : MonoBehaviour
{
    [SerializeField] float timeResetInterruped;
    public static UtilityAIManager UtilityAIManagerSingleton;

    public event Action OnInterrupedAction;
    public event Action OnCounterPlayer;

    public bool interruped { get; set; } = false;

    float countTimeReset;
    Health health;



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

        // health = 
    }



    void Update()
    {
        ResetInterrupped();
    }


    void ResetInterrupped()
    {
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
