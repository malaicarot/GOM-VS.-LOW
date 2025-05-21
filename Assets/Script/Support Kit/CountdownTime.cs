using System.Collections;
using UnityEngine;

public class CountdownTime : MonoBehaviour
{
    public static CountdownTime SingletonCountdown;
    float timer = 0;

    void Awake()
    {
        if (SingletonCountdown == null)
        {
            SingletonCountdown = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        Debug.Log("Countdown: " + timer);

    }


    public bool Countdown(float timeToCountdown)
    {
        if (timer <= 0)
        {
            timer = timeToCountdown;
            return true;
        }
        return false;
    }

    IEnumerator WaitToTime(float time)
    {
        timer = time;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
