using System;

using UnityEngine;


public class UtilityAIManager : Singleton<UtilityAIManager>
{
    [SerializeField] float timeResetInterruped;
    public event Action OnCounterPlayer;
    public event Action OnChangePhase;
    public bool interruped { get; set; } = false;
    public bool isPhaseTwo { get; set; } = false;


    int[] numberOfState = { 1, 2, 3 };

    float countTimeReset;
    Health health;


    void OnEnable()
    {
        health = GameObject.FindGameObjectWithTag("Boss")?.GetComponent<Health>();
    }

    void Update()
    {
        ResetInterrupped();
    }

    public void ChangePhase()
    {
        if (isPhaseTwo)
        {
            OnChangePhase?.Invoke();
        }
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

    public int RandomState()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);
        return randomNumber;
    }


    // void ChangePhaseTwo()
    // {
    //     isPhaseTwo = true;
    //     Debug.Log("Change Phase!");

    //     OnChangePhase?.Invoke();
    // }
}
