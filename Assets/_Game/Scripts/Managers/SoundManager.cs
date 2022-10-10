using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    jump = 0,
    hurt = 1,
    collision = 2,
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Audio[] audios;

    public void PlayAudio(AudioType audioType, float volume)
    {
        audioSource.PlayOneShot(audios[(int)audioType].audioClip, volume);
    }

    public void ToggleSound()
    {
        audioSource.mute = !audioSource.mute;
    }
}

[System.Serializable]
public struct Audio
{
    public AudioType soundType;
    public AudioClip audioClip;
}