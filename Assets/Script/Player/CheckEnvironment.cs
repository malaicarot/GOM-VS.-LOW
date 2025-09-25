using System;
using UnityEngine;

public class CheckEnvironment : MonoBehaviour
{
    [SerializeField] string tagName;
    public event Action OnInRiver;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            OnInRiver?.Invoke();
        }
    }
}
