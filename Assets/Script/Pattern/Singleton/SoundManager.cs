using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    AudioSource audioSource;

    public event Action PlayWeaponSound;
    void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            Destroy(soundManager);
            return;
        }
        else
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
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
