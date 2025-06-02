using System.Collections.Generic;
using UnityEngine;

public class AttackDealDamage : MonoBehaviour
{
    [SerializeField] Collider myCollider;
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

        alreadyCollider.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(dealDamaged);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * knockback);
        }
    }
}
