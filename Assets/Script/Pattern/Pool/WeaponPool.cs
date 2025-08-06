using UnityEngine;
using UnityEngine.Pool;

public class WeaponPool : ObjectPooling
{
    public static WeaponPool WeaponPoolSingleton;

    void Start()
    {
        if (WeaponPoolSingleton != null)
        {
            Destroy(WeaponPoolSingleton);
        }
        else
        {
            WeaponPoolSingleton = this;
        }
    }

    // public PooledObject GetWeapon(WeaponSO weaponSO, Vector3 position, Quaternion rotation)
    // {
    //     return GetPooledObject();
    // }
}
