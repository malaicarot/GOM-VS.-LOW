using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;
    public bool isFirstAttack = true;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

}
