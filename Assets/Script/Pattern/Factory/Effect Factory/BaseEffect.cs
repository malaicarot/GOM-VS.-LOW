using System;
using UnityEngine;

public abstract class Effect
{
    public abstract string Name { get; }
    public abstract void Proccess(GameObject caster);

}
