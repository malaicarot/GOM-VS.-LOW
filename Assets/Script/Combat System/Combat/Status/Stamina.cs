using System;
using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering;

public class Stamina : MonoBehaviour
{
    [SerializeField] float maxStamina = 100;
    [SerializeField] StatusBar statusBar;
    [SerializeField] float recoverySpeed;
    float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        statusBar.fillAmount = currentStamina / maxStamina;


    }


    public void RecoveryStamina(float amount)
    {
        currentStamina = Mathf.Min(currentStamina + amount, maxStamina);
    }


    public void ReduceStamina(float amount)
    {
        currentStamina = MathF.Max(currentStamina - amount, 0);
    }

}
