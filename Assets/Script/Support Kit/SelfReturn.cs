using System.Collections;
using UnityEngine;

public class SelfReturn : MonoBehaviour
{
    [SerializeField] float timeToReturn;
    void Start()
    {
        StartCoroutine(CountdownTime());
    }

    IEnumerator CountdownTime()
    {
        yield return new WaitForSecondsRealtime(timeToReturn);
        this.GetComponent<PooledObject>()?.Release();
    }
}
