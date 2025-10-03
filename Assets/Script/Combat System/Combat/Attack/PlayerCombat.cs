using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] AttackDealDamage attackDealDamage;



    void OnEnable()
    {
        attackDealDamage.OnHit += PlayerSingleton.Instance.PlaySFXWeaponHit;
    }
}
