using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] areaSpawn;
    [SerializeField] int maxAttemps = 10;
    [SerializeField] float radius = 5f;
    [SerializeField] int numberOfPoint = 5;
    [SerializeField] float timeToWait = 1f;


    void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(timeToWait);
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if (areaSpawn.Length <= 0) { return; }
        foreach (GameObject area in areaSpawn)
        {
            foreach (Vector3 position in GetRandomPointsInArea(area))
            {
                EnemyPool.EnemyPoolSingleton.GetEnemy(EnemyPool.EnemyPoolSingleton.RandomType(), position, Quaternion.identity);
            }
        }
    }

    List<Vector3> GetRandomPointsInArea(GameObject area)
    {
        List<Vector3> validTransform = new List<Vector3>();
        int found = 0;
        Bounds bounds = area.GetComponent<Collider>().bounds;
        while (found < numberOfPoint)
        {
            for (int i = 0; i < maxAttemps; i++)
            {
                Vector3 randomPoint = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                bounds.center.y,
                Random.Range(bounds.min.z, bounds.max.z)
                );
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
                {
                    found++;
                    validTransform.Add(hit.position);
                    break;
                }
            }
            if (found >= numberOfPoint)
            {
                break;
            }
        }
        return validTransform;
    }
}
