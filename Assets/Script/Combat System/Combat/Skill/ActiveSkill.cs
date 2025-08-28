using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    [SerializeField] GameObject VFX_1;
    [SerializeField] GameObject VFX_2;

    void OnEnable()
    {
        VFX_1.gameObject.SetActive(true);
        VFX_2.gameObject.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("VFX_1"))
        {
            VFX_1.gameObject.SetActive(false);
            VFX_2.gameObject.SetActive(true);
        }
    }
}
