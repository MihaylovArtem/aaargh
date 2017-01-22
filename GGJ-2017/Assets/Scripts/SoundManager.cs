using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip battleMusic;
    public AudioClip menuMusic; 

    public float volume;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetBattleMusic()
    {
        if (music.isPlaying) music.Stop();
        music.clip = battleMusic;
        music.volume = volume;
        music.Play();
    }

    public void SetMenuMusic()
    {
        if (music.isPlaying) music.Stop();
        music.clip = menuMusic;
        music.volume = volume;
        music.Play();
    }

    public bool IsPlaying { get { return music.isPlaying; } }
}
