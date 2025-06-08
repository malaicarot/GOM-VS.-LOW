
using UnityEngine;

public enum ENEMY_TYPE
{
    Enemy_Creep_1,
    Enemy_Creep_2,
    Enemy_Creep_3,
    Enemy_Boss_1
}
public class EnemyPool : ObjectPooling
{
    public static EnemyPool EnemyPoolSingleton;

    void Start()
    {
        // if (EnemyPoolSingleton != null)
        // {
        //     Destroy(EnemyPoolSingleton);
        // }
        // else
        // {
            EnemyPoolSingleton = this;
        // }
    }

    public PooledObject GetEnemy(string enemyType, Vector3 position, Quaternion rotation)
    {
        return GetPooledObject(enemyType, position, rotation);
    }

    public string RandomType()
    {
        int spawnRate = Random.Range(1, 4);
        switch (spawnRate)
        {
            case 1:
                return ENEMY_TYPE.Enemy_Creep_1.ToString();
            case 2:
                return ENEMY_TYPE.Enemy_Creep_2.ToString();
            case 3:
                return ENEMY_TYPE.Enemy_Creep_3.ToString();
            default:
                break;
        }
        return null;
    }
}
