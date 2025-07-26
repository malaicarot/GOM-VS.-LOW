using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] StatusBar statusBar;
    public event Action OnTakeDamage;
    public event Action OnDeath;

    public bool isDead => currentHealth == 0;

    PlayerStats playerStats;
    float currentHealth;
    int resistance;
    bool isParry;
    void Start()
    {
        if (gameObject.name == "Player")
        {
            
            playerStats = GetComponent<PlayerStats>();
            resistance = playerStats.ReturnAttribute("Resistance");
            maxHealth = playerStats.ReturnAttribute("Health");
            Debug.Log("resistance: " + resistance);
            Debug.Log("maxHealth: " + maxHealth);
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    public void SetHealth()
    {
        maxHealth = playerStats.ReturnAttribute("Health");
    }

    public void SetResistance()
    {
        resistance = playerStats.ReturnAttribute("Resistance");
        Debug.Log("Resistance value: " + resistance);
    }

    void Update()
    {
        if (statusBar != null)
        {
            statusBar.fillAmount = currentHealth / maxHealth;
        }

        if (this.gameObject.CompareTag("Boss"))
        {
            statusBar = GameObject.Find("BossHealthBar")?.GetComponent<StatusBar>();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

    }

    public void SetParry(bool isParry)
    {
        this.isParry = isParry;
    }

    public void RecoverHealth(float amount)
    {
        currentHealth = MathF.Min(currentHealth + amount, maxHealth);
    }

    public void DealDamage(int damage)
    {
        if (isParry) { return; }
        if (currentHealth == 0)
        {
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage + resistance, 0);
        OnTakeDamage?.Invoke();

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }
}
