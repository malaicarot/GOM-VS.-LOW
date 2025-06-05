using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class SpawnEnemy : PooledObject
{
    [SerializeField] GameObject areaSpawn;


    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            EnemyPool.EnemyPoolSingleton.GetEnemy("Enemy_Creep_1", areaSpawn.transform.position, Quaternion.identity);
        }
    }
}
