using UnityEngine;

public class EffectPool : ObjectPooling
{
    public static EffectPool EffectPoolSingleton;

    void Start()
    {
        if (EffectPoolSingleton != null)
        {
            Destroy(EffectPoolSingleton);
        }
        else
        {
            EffectPoolSingleton = this;
        }
    }


    public PooledObject GetEffect(string effectType, Vector3 position, Quaternion rotation)
    {
        return GetPooledObject(effectType, position, rotation, false);
    }
}
