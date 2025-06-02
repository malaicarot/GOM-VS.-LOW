using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public event Action OnTakeDamage;
    public event Action OnDeath;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke();
        Debug.Log($"{gameObject.name}: {currentHealth}");
    }
}
