using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] StatusBar statusBar;
    public event Action OnTakeDamage;
    public event Action OnDeath;
    public event Action OnStun;
    public event Action OnHardCC;
    public event Action OnProcessBloodThreshold;

    public bool isDead => currentHealth == 0;
    public bool isCounterPlayer { get; set; } = false;
    public bool isHardCC { get; set; } = false;
    public bool isChangingPhase = false;

    

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
    }

    public bool GetHealthToChangeState()
    {
        return currentHealth <= maxHealth / 2;
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

        if (isCounterPlayer)
        {
            OnStun?.Invoke();
            isCounterPlayer = false;
        }
        else if (isHardCC)
        {
            OnHardCC?.Invoke();
            isHardCC = false;
        }
        else
        {
            OnTakeDamage?.Invoke();
        }

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }

        if (!isChangingPhase)
        {
            if (GetHealthToChangeState())
            {
                OnProcessBloodThreshold?.Invoke();
                isChangingPhase = true;
            }
        }
    }
}
