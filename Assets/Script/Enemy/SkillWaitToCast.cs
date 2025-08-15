using System.Collections;
using UnityEngine;

public class SkillWaitToCast : MonoBehaviour
{
    [SerializeField] Collider skillCollider;
    [SerializeField] float timeToWait;
    void OnEnable()
    {
        StartCoroutine(WaitToCast());
    }

    IEnumerator WaitToCast()
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        skillCollider.enabled = true;
    }
}
