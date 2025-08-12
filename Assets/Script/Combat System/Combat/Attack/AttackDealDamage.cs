using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackDealDamage : MonoBehaviour
{
    public Collider myCollider;
    List<Collider> alreadyCollider = new List<Collider>();
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
        if (alreadyCollider.Contains(other)) { return; }
        if (this.tag == other.tag) { return; }

        alreadyCollider.Add(other);


        if (other.TryGetComponent<Health>(out Health health))
        {
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

        if (other.TryGetComponent<Target>(out Target target))
        {
            if (target.isFirstAttack)
            {
                target.isFirstAttack = false;
            }
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * knockback);
        }
    }
}
