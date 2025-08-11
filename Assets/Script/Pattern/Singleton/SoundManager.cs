using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        audioSource.Stop();
        audioSource.Play();
    }

    public void StopSound(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        audioSource.Stop();
    }


}
