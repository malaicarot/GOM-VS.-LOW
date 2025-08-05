using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public bool isPerfect { get; private set; }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyAttack")) { return; }
        isPerfect = true;
        Debug.Log(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("EnemyAttack")) { return; }
        isPerfect = false;
        Debug.Log(other.gameObject.name);
    }
}
