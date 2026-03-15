using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

    public Sound[] sfx, music;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [SerializeField] 
    private GameObject XIconMusic;
    [SerializeField] 
    private GameObject XIconSFX;
    
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        MusicVolume(0.5f);
        SFXVolume(0.5f);
        PlayMusic("Music1");
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        
        XIconMusic.SetActive(!XIconMusic.activeSelf);
        
        if (musicSource.mute) musicSource.Pause();
        else musicSource.UnPause();
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        
        XIconSFX.SetActive(!XIconSFX.activeSelf);
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public static void PlayMusic(string name)
    {
        Sound sound = Array.Find(instance.music, x => x.name == name);

        if (sound != null)
        {
            instance.musicSource.clip = sound.clip;
            instance.musicSource.Play();
        }
    }
    public static void PlaySFX(string name)
    {
        Sound sound = Array.Find(instance.sfx, x => x.name == name);

        if (sound != null)
        {
            instance.sfxSource.PlayOneShot(sound.clip);
        }
    }
}
