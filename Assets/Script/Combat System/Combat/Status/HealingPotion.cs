using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] StatusBar statusBar;
    [SerializeField] float maxPotion;
    public float currentPotion{ get; set; }

    void Start()
    {
        currentPotion = maxPotion;
    }

    void Update()
    {
        statusBar.fillAmount = currentPotion / maxPotion;
    }

    public void ReducePotion(float amount)
    {
        if (currentPotion <= 0)
        {
            return;
        }
        currentPotion = Mathf.Min(currentPotion - amount, maxPotion);
    }
}
