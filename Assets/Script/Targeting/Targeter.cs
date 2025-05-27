using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();
    public Target currentTarget;


    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Remove(target);
    }

    public bool SelectedTarget()
    {
        if (targets.Count <= 0) { return false; }
        currentTarget = targets[0];
        
        return true;
    }

    public void CancelTarget()
    {
        currentTarget = null;
    }
}
