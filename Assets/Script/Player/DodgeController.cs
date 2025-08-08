using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    [SerializeField] float perfectDodgeRadius = 0.5f;
    [SerializeField] float perfectDuration = 0.4f;

    public bool isPerfect { get; private set; }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyAttack")) { return; }
        StartCoroutine(TriggerIsPerfect());
    }

    IEnumerator TriggerIsPerfect()
    {
        isPerfect = true;
        yield return new WaitForSecondsRealtime(perfectDuration);
        isPerfect = false;
    }
}
