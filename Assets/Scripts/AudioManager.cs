using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameEndClip;
    public AudioClip coinCollectClip;
    public AudioClip backgroundMusic;
    public AudioClip jumpClip;

    private AudioSource bgmSource; // For background music
    private AudioSource sfxSource; // For sound effects

    void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.clip = backgroundMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayGameEndAudio()
    {
        bgmSource.Stop(); 
        sfxSource.PlayOneShot(gameEndClip); 
    }

    public void CoinCollectAudio()
    {
        sfxSource.PlayOneShot(coinCollectClip);
    }

    public void JumpAudio()
    {
        sfxSource.PlayOneShot(jumpClip);
    }

    public void PauseAudio()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    public void ResumeAudio()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }
}
