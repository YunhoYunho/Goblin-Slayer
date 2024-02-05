using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX { Attack, EarthQuake, RestoreHP }

public class SoundManager : SingleTon<SoundManager>
{
    [Header("BGM")]
    [SerializeField]
    private AudioClip bgmClip;
    [SerializeField]
    private AudioSource bgmSource;

    [Header("SFX")]
    [SerializeField]
    private AudioClip[] sfxClips;
    [SerializeField]
    private AudioSource[] sfxSources;
    private int index;

    public void PlayBGM(bool isPlaying)
    {
        if (isPlaying)
            bgmSource.Play();
        else
            bgmSource.Stop();
    }

    public void PlaySFX(SFX sfx)
    {
        index = (index + 1) % sfxSources.Length;
        sfxSources[index].PlayOneShot(sfxClips[(int)sfx]);
    }
}
