using UnityEngine;

public class SetUpButtonPanel : MonoBehaviour
{
    [SerializeField] Transform slotParent;
    [SerializeField] GameObject prefabs;

    void OnEnable()
    {
        foreach (var specialEffect in SpecialEffectManagers.Instance.specialEffectsDataList)
        {
            var slotPrefab = Instantiate(prefabs, slotParent);
            SpecialAttackUI specialAttackUI = slotPrefab.GetComponent<SpecialAttackUI>();
            specialAttackUI.Init(specialEffect);
        }
    }
}
