using UnityEngine;

public class SpecialAttackPanel : MonoBehaviour
{
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject prefabs;

    void OnEnable()
    {
        foreach (var specialEffect in SpecialEffectManagers.specialEffectManagers.specialEffectsDataList)
        {
            var slotPrefab = Instantiate(prefabs, slotParent);
            SpecialAttackUI specialAttackUI = slotPrefab.GetComponent<SpecialAttackUI>();
            specialAttackUI.Init(specialEffect);
        }
    }
}
