using System;
using UnityEngine;


[Serializable]
public class Attack
{
    [field: SerializeField] public string AttackAnimationName { get; private set; }
    [field: SerializeField] public float AnimationDuration { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
    [field: SerializeField] public int AttackIndex { get; private set; } = -1;

}
