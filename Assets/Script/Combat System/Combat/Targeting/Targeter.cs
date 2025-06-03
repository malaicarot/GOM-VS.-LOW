using System;
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
    public Target currentTarget { get; private set; }
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }


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

        Target closetsTarget = null;
        float closetsDistance = Mathf.Infinity;


        foreach (Target target in targets)
        {
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(target.transform.position);
            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }
            Vector2 toCenter = viewPosition - new Vector2(0.5f, 0.5f);

            if (toCenter.sqrMagnitude < closetsDistance)
            {
                closetsTarget = target;
                closetsDistance = toCenter.sqrMagnitude;
            }
            
        }
        if(closetsTarget == null){ return false; }

        currentTarget = closetsTarget;
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
