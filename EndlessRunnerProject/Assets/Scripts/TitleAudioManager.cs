using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{

    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----------Audio Clip----------")]
    public AudioClip mainMenuTheme;
    public AudioClip creditsTheme;
    public AudioClip highscoreTheme;
    public AudioClip buttonHighlightSound;
    public AudioClip buttonClickSound;


    private bool gameOverSoundPlayed = false;

    public float fadeDuration = 1.25f;

    private void Start()
    {
        musicSource.clip = mainMenuTheme;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {

        sfxSource.PlayOneShot(clip);

    }

    public void OnCreditsButtonClicked()
    {
        StartCoroutine(FadeOutMainMenuAndFadeInCredits());
    }

    private IEnumerator FadeOutMainMenuAndFadeInCredits()
    {

        float timeElapsed = 0f;
        float timeToFade = 1.25f;

        while (timeElapsed < timeToFade)
        {

            musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / timeToFade);
            musicSource.Stop();
            musicSource.clip = creditsTheme;
            musicSource.Play();
            musicSource.volume = Mathf.Lerp(0f, 1f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

    }

    public void OnBackButtonClicked()
    {
        StartCoroutine(FadeOutCreditsAndFadeInMainMenu());
    }

    private IEnumerator FadeOutCreditsAndFadeInMainMenu()
    {
        float timeElapsed = 0f;
        float timeToFade = 1.25f;

        while (timeElapsed < timeToFade)
        {

            musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / timeToFade);
            musicSource.Stop();
            musicSource.clip = mainMenuTheme;
            musicSource.Play();
            musicSource.volume = Mathf.Lerp(0f, 1f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

    }


}
