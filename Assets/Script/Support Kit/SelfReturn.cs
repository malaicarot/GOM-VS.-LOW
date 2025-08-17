using System;
using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(PooledObject))]
public class SelfReturn : MonoBehaviour
{
    [SerializeField] float timeToReturn;
    float time;

    void Start()
    {
        time = timeToReturn;
    }

    void OnDisable()
    {
        time = timeToReturn;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            this.GetComponent<PooledObject>()?.Release();
        }
    }
}
