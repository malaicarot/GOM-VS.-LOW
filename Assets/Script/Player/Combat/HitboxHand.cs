using UnityEngine;

public class HitboxHand : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
        }
    }
}
