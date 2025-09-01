using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AttackDealDamage : MonoBehaviour
{
    public Collider myCollider;
    List<Collider> alreadyCollider = new List<Collider>();
    public event Action OnDissolve;
    int dealDamaged;
    float knockback;

    void OnEnable()
    {
        alreadyCollider.Clear();
    }

    public void SetAttack(int damage, float knockback)
    {
        dealDamaged = damage;
        this.knockback = knockback;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }
        if (alreadyCollider.Contains(other) && !this.CompareTag("DPS")) { return; }
        if (this.tag == other.tag) { return; }

        alreadyCollider.Add(other);

        if (other.CompareTag("Parry"))
        {
            return;
        }

        if (this.CompareTag("DragonAttack") && (other.CompareTag("Boss") || other.CompareTag("Enemy")))
        {
            OnDissolve?.Invoke();
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            if (this.CompareTag("Hard_CC"))
            {
                health.isHardCC = true;
            }

            if (this.CompareTag("EnemySpecialAttack"))
            {
                health.isCounterPlayer = true;
            }
            health.DealDamage(dealDamaged);
        }

        if (other.TryGetComponent<AttackHandler>(out AttackHandler attackHandler))
        {
            attackHandler.OnDisableAttackRight();
            attackHandler.OnDisableAttackLeft();
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * knockback);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * knockback);
        }


        if (other.TryGetComponent<Health>(out Health health))
        {
            if (this.CompareTag("DPS"))
            {
                health.DealDamage(dealDamaged);
            }
        }
    }
}
