using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] List<AudioClip> audioClips;

    Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    void Start()
    {
        foreach (AudioClip audioClip in audioClips)
        {
            if (!sfxDictionary.ContainsKey(audioClip.name))
            {
                sfxDictionary.Add(audioClip.name, audioClip);
            }
        }
    }


    public void PlaySound(AudioClip audioClip)
    {
        if (sfxAudioSource != null && audioClip != null)
        {
            sfxAudioSource.clip = audioClip;
        }
        sfxAudioSource.Stop();
        sfxAudioSource.Play();
    }


    public void PlaySFX(string sfxType, bool isRandomPitch = false)
    {
        if (sfxDictionary.TryGetValue(sfxType, out AudioClip audioClip))
        {
            if (isRandomPitch)
            {
                sfxAudioSource.pitch = Random.Range(0.9f, 1.1f);
            }
            else
            {
                sfxAudioSource.pitch = 1f;
            }
            sfxAudioSource.PlayOneShot(audioClip);
        }
    }

}
