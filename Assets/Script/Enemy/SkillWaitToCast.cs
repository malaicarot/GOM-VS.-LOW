using System.Collections;
using UnityEngine;

public class SkillWaitToCast : MonoBehaviour
{
    [SerializeField] GameObject skillCollider;
    [SerializeField] float timeToWait;
    bool isActive = false;
    float time;

    void OnEnable()
    {
        isActive = true;
    }

    void OnDisable()
    {
        isActive = false;
        time = 0;
        skillCollider.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            if (time >= timeToWait)
            {
                skillCollider.SetActive(true);
            }
        }
    }
}
