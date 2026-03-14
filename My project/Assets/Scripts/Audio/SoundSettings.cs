using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
        AudioManager.PlaySFX("click");
    }
    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        AudioManager.PlaySFX("click");
    }
    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        AudioManager.PlaySFX("click");
    }
}

