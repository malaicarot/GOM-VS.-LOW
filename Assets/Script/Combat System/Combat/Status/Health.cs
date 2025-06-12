using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] StatusBar statusBar;
    public event Action OnTakeDamage;
    public event Action OnDeath;
    public bool isDead => currentHealth == 0;
    float currentHealth;
    bool isParry;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (gameObject.tag == "Player")
        {
            statusBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void SetParry(bool isParry)
    {
        this.isParry = isParry;
    }

    public void RecoverHealth(float amount)
    {
        currentHealth = MathF.Min(currentHealth + amount, maxHealth);
    }

    public void DealDamage(float damage)
    {
        if (isParry) { return; }
        if (currentHealth == 0)
        {
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke();

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }
}
