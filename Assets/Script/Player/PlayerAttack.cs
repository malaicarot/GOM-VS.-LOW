using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Collider leftHand;
    [SerializeField] Collider rightHand;

    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 2;
    [SerializeField] LayerMask enemyLayer;

    void Awake()
    {
        leftHand.enabled = false;
        rightHand.enabled = false;
    }


    public void EnableHitboxes()
    {
        leftHand.enabled = true;
        rightHand.enabled = true;
    }

    public void DisableHitboxes()
    {
        leftHand.enabled = false;
        rightHand.enabled = false;
    }


}
