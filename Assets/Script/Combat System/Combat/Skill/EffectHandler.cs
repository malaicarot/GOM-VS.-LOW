using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float time;
    float currentTime;

    void OnEnable()
    {
        currentTime = time;
    }


    void OnDisable()
    {
        effect.gameObject.SetActive(true);
        time = currentTime;
        
    }


    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            effect.gameObject.SetActive(false);
        }
    }
}
