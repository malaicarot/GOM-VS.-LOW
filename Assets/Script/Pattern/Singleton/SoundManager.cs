using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource playerActionAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource musicAudioSource;


    [SerializeField] List<AudioClip> playerAudioClips;
    [SerializeField] List<AudioClip> sfxAudioClips;

    Dictionary<string, AudioClip> playerDictionary = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    void Start()
    {
        ////Player
        foreach (AudioClip audioClip in playerAudioClips)
        {
            if (!playerDictionary.ContainsKey(audioClip.name))
            {
                playerDictionary.Add(audioClip.name, audioClip);
            }
        }


        //// SFX
        foreach (AudioClip audioClip in sfxAudioClips)
        {
            if (!sfxDictionary.ContainsKey(audioClip.name))
            {
                sfxDictionary.Add(audioClip.name, audioClip);
            }
        }
    }



    public void PlayerActionSound(string sfxType, bool isRandomPitch = false)
    {
        if (playerDictionary.TryGetValue(sfxType, out AudioClip audioClip))
        {
            if (isRandomPitch)
            {
                playerActionAudioSource.pitch = Random.Range(0.9f, 1.1f);
            }
            else
            {
                playerActionAudioSource.pitch = 1f;
            }
            playerActionAudioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySFX(string sfxType)
    {
        if (sfxDictionary.TryGetValue(sfxType, out AudioClip audioClip))
        {
            sfxAudioSource.PlayOneShot(audioClip);
        }
    }

}
