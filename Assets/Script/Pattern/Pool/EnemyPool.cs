using UnityEngine;

public class EnemyPool : ObjectPooling
{
    public static EnemyPool EnemyPoolSingleton;

    void Start()
    {
        if (EnemyPoolSingleton != null)
        {
            Destroy(EnemyPoolSingleton);
        }
        else
        {
            EnemyPoolSingleton = this;
        }
    }


    public PooledObject GetEnemy(string enemyType, Vector3 position, Quaternion rotation)
    {
        return GetPooledObject(enemyType, position, rotation);
    }

}
