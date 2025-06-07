using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class SpawnEnemy : PooledObject
{
    [SerializeField] Transform[] areaSpawn;
    [Range(1, 10), SerializeField] uint enemiesQuantity;
    [SerializeField] Vector3 distanceBetweenEnemies;


    Vector3 rootPosition;


    void Start()
    {
        SpawnEnemies();
    }


    void SpawnEnemies()
    {
        foreach (Transform areaPosition in areaSpawn)
        {
            rootPosition = areaPosition.position;
            for (int i = 0; i < enemiesQuantity; i++)
            {
                Debug.Log(rootPosition);
                PooledObject enemy = EnemyPool.EnemyPoolSingleton.GetEnemy(EnemyPool.EnemyPoolSingleton.RandomType(), rootPosition, Quaternion.identity);
                rootPosition += distanceBetweenEnemies;
                // FallToGround(enemy);
            }
        }
    }


 
}
