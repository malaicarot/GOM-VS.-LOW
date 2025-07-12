using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    float maxStamina;
    [SerializeField] StatusBar statusBar;
    [SerializeField] float recoverySpeed;
    PlayerStats playerStats;
    float currentStamina;

    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        SetStamina();
    }

    void Update()
    {
        statusBar.fillAmount = currentStamina / maxStamina;
    }

    public void SetStamina()
    {
        maxStamina = playerStats.ReturnAttribute("Stamina");
        currentStamina = maxStamina;
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;

    }


    public void RecoveryStamina(float amount)
    {
        currentStamina = Mathf.Lerp(currentStamina, Mathf.Min(currentStamina + amount, maxStamina), recoverySpeed);
    }

    public void ReduceStamina(float amount)
    {
        currentStamina = MathF.Max(currentStamina - amount, 0);
    }

}
