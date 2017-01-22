using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip battleMusic;
    public AudioClip menuMusic;
    float fadeTimer = 1f;
    float fadeDuration = 0.5f;

    public bool IsFadeOut { get; set; }

    public float volume;

    void Awake()
    {
        IsFadeOut = true;
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
    

    void FixedUpdate()
    {
        fadeTimer += Time.fixedDeltaTime;
        var pitch = Mathf.Lerp(IsFadeOut ? 0 : 1, IsFadeOut ? 1 : 0, fadeTimer / fadeDuration);
        music.pitch = pitch;     
    }

    public void FadeInBattle()
    {
        fadeTimer = 0f;
        IsFadeOut = false;
    }

    public void FadeOutBattle()
    {
        fadeTimer = 0f;
        IsFadeOut = true;
    }

    public bool IsPlaying { get { return music.isPlaying; } }
}
