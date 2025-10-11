using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] AttackDealDamage attackDealDamage;
    [SerializeField] AttackDealDamage attackSubDealDamage;

    void OnEnable()
    {
        attackDealDamage.OnHit += PlayerSingleton.Instance.PlaySFXWeaponHit;
        attackSubDealDamage.OnHit += PlayerSingleton.Instance.PlaySFXSubWeaponHit;
    }
}
