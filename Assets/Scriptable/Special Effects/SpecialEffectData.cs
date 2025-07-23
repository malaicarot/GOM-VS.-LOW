using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialEffects", menuName = "Scriptable Objects/SpecialEffectsData")]

public class SpecialEffectsData : ScriptableObject
{
    [Header("General Infor")]
    public string effectName;
    [TextArea, SerializeField] public string Description;
    public Sprite Thumbnail;

    [Header("Logic")]
    public AnimationClip Animation;
    public int AnimationHash = Animator.StringToHash("FirstHit");
    public string AnimationTag = "FirstHit";

    // public event Action OnEffectTrigger;

    [Header("Effect")]
    public int AttackDamage = 20;
    public float AttackKnockback = 40f;
    public float attackRangeBonus;
    public float staminaRegen;

    [Header("State")]
    public bool unlocked = false;

    // public void ActiveAtion()
    // {
    //     if (unlocked)
    //     {
    //         OnEffectTrigger?.Invoke();
    //     }
    // }


}
