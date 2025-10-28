
using System;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] StatusBar statusBar;
    [SerializeField] float maxPotion;
    public float currentPotion { get; set; }


    // Action for special effect



    void Start()
    {
        currentPotion = maxPotion;
    }

    void Update()
    {
        statusBar.value = currentPotion / maxPotion;
    }

    public void ResetPotion()
    {
        currentPotion = maxPotion;

    }

    public void ReducePotion(float amount)
    {
        if (currentPotion <= 0)
        {
            return;
        }
        currentPotion = Mathf.Min(currentPotion - amount, maxPotion);
    }

    //Special Effect when attach item from brew

    public void UltimateHealing()
    {
        // OnUltimateHealing?.Invoke();
    }
    public void RestoreMana()
    {

    }
    public void CriticalTime()
    {

    }
    public void SetCooldown()
    {

    }
}
