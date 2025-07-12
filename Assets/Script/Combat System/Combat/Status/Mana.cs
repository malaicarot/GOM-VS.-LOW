using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Mana : MonoBehaviour
{
    float maxMana = 100f;
    [SerializeField] StatusBar statusBar;
    PlayerStats playerStats;

    public float currentMana { get; set; }

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        SetMana();
    }

    void Update()
    {
        if (statusBar != null)
        {
            statusBar.fillAmount = currentMana / maxMana;

        }
    }

    public void SetMana()
    {
        maxMana = playerStats.ReturnAttribute("Mana");
        currentMana = maxMana;
    }


    public void ResetMana()
    {
        currentMana = maxMana;
    }

    public void IncreaseMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }


    public void ReduceMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
    }
}
