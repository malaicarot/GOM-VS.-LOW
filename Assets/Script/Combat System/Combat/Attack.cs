using System;
using UnityEngine;


[Serializable]
public class Attack
{
    [field: SerializeField] public string AttackAnimationName { get; private set; }
    [field: SerializeField] public float AnimationDuration { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
    [field: SerializeField] public int AttackIndex { get; private set; } = -1;
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }


}
