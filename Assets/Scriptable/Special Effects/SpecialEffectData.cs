using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialEffects", menuName = "Scriptable Objects/SpecialEffectsData")]

public class SpecialEffectsData : ScriptableObject
{
    [Header("General Infor")]
    public string EffectName;
    [TextArea, SerializeField] public string Description;
    public Sprite Thumbnail;

    [Header("Logic")]
    public AnimationClip Animation;
    public int AnimationHash = Animator.StringToHash("FirstHit");
    public string AnimationTag = "FirstHit";

    [Header("Effect")]
    public float AttackKnockback = 40f;
    public int AttackCoefficient = 2;

    [Header("State")]
    public bool unlocked = false;
}
