using System.Collections.Generic;
using UnityEngine;

public class AttackDealDamage : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    List<Collider> alreadyCollider = new List<Collider>();
    int dealDamaged;


    void OnEnable()
    {
        alreadyCollider.Clear();
    }
    public void SetAttack(int damage)
    {
        dealDamaged = damage;
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
    }


}
