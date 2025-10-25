using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] float increaseMana = 10;
    [SerializeField] StatusBar statusBar;
    PlayerStats playerStats;

    public float currentMana { get; set; }

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (statusBar != null)
        {
            statusBar.value = currentMana / playerStats.ReturnAttribute("Mana");
        }
    }

    public void SetMana()
    {
        currentMana += increaseMana;
    }


    public void ResetMana()
    {
        currentMana = 0;
    }

    public void IncreaseMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, playerStats.ReturnAttribute("Mana"));
    }

    public void ReduceMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
    }
}
