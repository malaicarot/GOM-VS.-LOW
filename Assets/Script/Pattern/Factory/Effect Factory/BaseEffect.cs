using System;
using UnityEngine;

public abstract class Effect
{
    public abstract string Name { get; }
    public abstract void Proccess(SpecialEffectsData effectsData, GameObject caster);

    // public abstract void Effective(Action<SpecialEffectsData, GameObject> callback);
}
