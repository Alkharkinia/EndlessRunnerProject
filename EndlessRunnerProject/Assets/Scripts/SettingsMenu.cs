using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Mixers")]
    public AudioMixer audioMixer;

    [Header("UI Sliders")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Exposed Parameters in Audio Mixer")]
    public string musicVolumeParameter = "MusicVolume";  // Name of the exposed parameter for music
    public string sfxVolumeParameter = "SFXVolume";      // Name of the exposed parameter for SFX

    private void Start()
    {
        // Set the initial volume levels for music and SFX
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));

        // Add listeners for the sliders to update volume
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);  // Save the music volume setting
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SFX", sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);  // Save the SFX volume setting
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
