using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EffectFactory
{
    private static Dictionary<string, Type> effectByName;
    private static bool IsInitialized => effectByName != null;


    public static void IsInitializeEffect()
    {

        if (IsInitialized) { return; }
        var effectTypes = Assembly.GetAssembly(typeof(Effect)).GetTypes()
        .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Effect)));

        effectByName = new Dictionary<string, Type>();

        foreach (var type in effectTypes)
        {
            var tempEffect = Activator.CreateInstance(type) as Effect;

            effectByName.Add(tempEffect.Name, type);
        }
    }


    public static Effect GetEffect(string effectType)
    {
        IsInitializeEffect();
        if (effectByName.ContainsKey(effectType))
        {
            Type type = effectByName[effectType];
            var effect = Activator.CreateInstance(type) as Effect;
            return effect;
        }
        return null;
    }

    internal static IEnumerable<string> GetEffectNames()
    {
        Debug.Log("Test");
        IsInitializeEffect();
        return effectByName.Keys;
    }

}