using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnTransform { get; private set; }
    public bool isGetCheckpoint { get; private set; } = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            isGetCheckpoint = true;
            respawnTransform = other.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            isGetCheckpoint = false;
            respawnTransform = null;
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(2f);
    }
}
