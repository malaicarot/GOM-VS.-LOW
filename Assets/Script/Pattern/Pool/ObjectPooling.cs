using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPooling : MonoBehaviour
{
    [Range(1, 50), SerializeField] uint poolSize;
    [SerializeField] List<PooledObject> pooledObjects;
    Dictionary<string, Stack<PooledObject>> pooledObjectDictionary;


    void Awake()
    {
        SetupPool();
    }

    void SetupPool()
    {
        if (pooledObjects == null || pooledObjects.Count == 0)
        {
            return;
        }

        pooledObjectDictionary = new Dictionary<string, Stack<PooledObject>>();
        foreach (PooledObject pooledObject in pooledObjects)
        {
            Stack<PooledObject> pooledStack = new Stack<PooledObject>();
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject instance = Instantiate(pooledObject);
                instance.gameObject.transform.parent = this.transform;
                instance._Instance = this;
                instance.gameObject.SetActive(false);
                instance.name = pooledObject.name;
                pooledStack.Push(instance);
            }
            pooledObjectDictionary.Add(pooledObject.name, pooledStack);
        }
    }

    public PooledObject GetPooledObject(string type, Vector3 position, Quaternion rotation)
    {
        if (!pooledObjectDictionary.ContainsKey(type) || String.IsNullOrEmpty(type))
        {
            return null;
        }

        // Nếu có trong Dictionary và không còn trong pool
        if (pooledObjectDictionary[type].Count == 0)
        {
            PooledObject urgentInstance = Instantiate(pooledObjects.Find(pooledObject => pooledObject.name == type));
            urgentInstance._Instance = this;
            urgentInstance.name = type;
            SetTransform(urgentInstance, position, rotation);
            return urgentInstance;
        }

        // Nếu có trong Dictionary và vẫn còn trong pool
        PooledObject nextInstance = pooledObjectDictionary[type].Pop();
        SetTransform(nextInstance, position, rotation);
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        if (!pooledObjectDictionary.ContainsKey(pooledObject.name))
        {
            Destroy(pooledObject);
        }

        pooledObject.gameObject.transform.position = Vector3.zero;
        pooledObject.gameObject.transform.rotation = Quaternion.identity;
        pooledObject.gameObject.SetActive(false);
        pooledObjectDictionary[pooledObject.name].Push(pooledObject);
    }

    void SetTransform(PooledObject pooledObject, Vector3 position, Quaternion rotation)
    {
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(position, out navMeshHit, 10f, NavMesh.GetAreaFromName("Walkable")))
        {
            pooledObject.gameObject.transform.position = navMeshHit.position;
        }
        else
        {
            pooledObject.gameObject.transform.position = position;
        }

        pooledObject.gameObject.transform.rotation = rotation;
    }
}
