using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 2;
    [SerializeField] LayerMask enemyLayer;

    public void OnAttackHit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name);

        }

    }

    void OnDrawGizmosSelected()
    {
        // if (attackPoint == null) return;
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
