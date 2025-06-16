using UnityEngine;

public class TriggerMeetBoss : MonoBehaviour
{
    [SerializeField] GameObject bossHealthBar;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossHealthBar.SetActive(true);
        }
    }
}
