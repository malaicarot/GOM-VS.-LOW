using UnityEngine;

[CreateAssetMenu(fileName = "SpecialEffects", menuName = "Scriptable Objects/SpecialEffectsData")]

public class SpecialEffectsData : ScriptableObject
{
    [Header("General Infor")]
    public string effectName;
    [TextArea, SerializeField] string Description;
    public Sprite Thumbnail;

    [Header("Logic")]
    public AnimationClip Animation;
    public enum TriggerType { OnAttack, OnFirstHit, OnCritical }
    public TriggerType trigger;

    [Header("Effect")]
    public float attackRangeBonus;
    public float staminaRegen;

    public bool unlocked = false;

}
