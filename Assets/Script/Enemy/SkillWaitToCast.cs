using System.Collections;
using UnityEngine;

public class SkillWaitToCast : MonoBehaviour
{
    [SerializeField] GameObject skillCollider;
    [SerializeField] float timeToWait;
    bool isActive;
    float time;

    void OnEnable()
    {
        time = timeToWait;
        isActive = true;
    }

    void OnDisable()
    {
        isActive = false;
        timeToWait = time;
        skillCollider.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0)
            {
                skillCollider.SetActive(true);
            }
        }
    }
}
