using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] Transform[] areaSpawn;
    [Range(1, 10), SerializeField] uint enemiesQuantity;
    [SerializeField] Vector3 distanceBetweenEnemies;

    Vector3 rootPosition;
    List<Transform> validTransform = new List<Transform>();


    void Start()
    {
        FilterValidPoint();
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(1f);
        SpawnEnemies();
    }

    void FilterValidPoint()
    {
        foreach (Transform item in areaSpawn)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(item.position, out hit, 5f, NavMesh.AllAreas))
            {
                validTransform.Add(item);
            }
        }
    }

    void SpawnEnemies()
    {
        if (validTransform.Count <= 0) { return; }
        foreach (Transform areaPosition in validTransform)
        {
            rootPosition = areaPosition.position;
            for (int i = 0; i < enemiesQuantity; i++)
            {
                EnemyPool.EnemyPoolSingleton.GetEnemy(EnemyPool.EnemyPoolSingleton.RandomType(), rootPosition, Quaternion.identity);
                rootPosition += distanceBetweenEnemies;
            }
        }
    }
}
