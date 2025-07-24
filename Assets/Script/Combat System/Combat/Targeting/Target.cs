using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;
    public bool isFirstAttack = true;
    float timer = 8f;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    void Update()
    {
        if (isFirstAttack == false)
        {
            StartCoroutine(CountDownToReturnFirstTarget());
        }
    }

    IEnumerator CountDownToReturnFirstTarget()
    {
        yield return new WaitForSeconds(timer);
        isFirstAttack = true;
    }
}
