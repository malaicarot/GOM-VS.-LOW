using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    SpecialEffectsData specialEffectsData;


    public void Init(SpecialEffectsData _specialEffectsData)
    {
        specialEffectsData = _specialEffectsData;
        image.sprite = _specialEffectsData.Thumbnail;
        // text.text = _specialEffectsData.effectName;
        GetComponent<Button>().onClick.AddListener(OnclickSlot);
        Refesh();
    }

    void OnclickSlot()
    {
        if (!specialEffectsData.unlocked)
        {
            SpecialEffectManagers.specialEffectManagers.UnlockEffect(specialEffectsData.effectName);
            Refesh();
        }
    }

    void Refesh()
    {
        image.color = specialEffectsData.unlocked ? Color.white : Color.gray;
    }
}
