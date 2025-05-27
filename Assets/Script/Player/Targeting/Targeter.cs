using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] float targetRadius = 2f;
    [SerializeField] float targetWeight = 0.25f;
    List<Target> targets = new List<Target>();
    public Target currentTarget{ get; private set; }


    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
    }

    public bool SelectedTarget()
    {
        if (targets.Count <= 0) { return false; }

        currentTarget = targets[0];
        cinemachineTargetGroup.AddMember(currentTarget.transform, targetWeight, targetRadius);
        return true;
    }

    public void CancelTarget()
    {
        if (cinemachineTargetGroup == null) { return; }
        cinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }


    void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cinemachineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
