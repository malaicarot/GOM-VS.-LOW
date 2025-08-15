using System;
using UnityEngine;

public class UtilityAIManager : Singleton<UtilityAIManager>
{
    [SerializeField] float timeResetInterruped;
    public event Action OnCounterPlayer;
    public bool interruped { get; set; } = false;

    float countTimeReset;
    Health health;


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
