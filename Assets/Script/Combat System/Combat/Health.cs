using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public event Action OnTakeDamage;
    public event Action OnDeath;
    public bool isDead => currentHealth == 0;
    int currentHealth;
    bool isParry;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetParry(bool isParry)
    {
        this.isParry = isParry;
    }

    public void DealDamage(int damage)
    {
        if (isParry) { return; }
        if (currentHealth == 0)
        {
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke();
        Debug.Log($"{gameObject.name}: {currentHealth}");

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }
}
