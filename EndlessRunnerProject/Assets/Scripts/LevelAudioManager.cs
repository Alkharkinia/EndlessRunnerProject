using System.Collections;
using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;   // The AudioSource for background music
    [SerializeField] AudioSource sfxSource;     // The AudioSource for sound effects (e.g., jumping, game over)

    [Header("----------Audio Clips----------")]
    public AudioClip levelTheme;      // Level music theme
    public AudioClip jumpSFX;         // Jump sound effect
    public AudioClip collisionSFX;         // Jump sound effect
    public AudioClip coinSFX;         // Coin collection sound effect
    public AudioClip playerDeathSFX;     // Game over sound effect
    public AudioClip enemyDeathSFX;     // Game over sound effect
    public AudioClip playerWinSFX;     // Game over sound effect
    public AudioClip enemyWinSFX;     // Game over sound effect
    public AudioClip invincibilitySFX;     // Game over sound effect
    public AudioClip buttonClickSFX;  // Button click sound effect

    private bool gameOverSoundPlayed = false;

    public float fadeDuration = 1.25f;

    private void Start()
    {
        musicSource.clip = levelTheme;
        musicSource.Play();
        musicSource.loop = true;  // Loop the level theme

    }


    // Play a one-shot sound effect (like jumping, collecting items, etc.)
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoopingSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = true;  // Loop the sound effect
        sfxSource.Play();
    }

    public void StopLoopingSFX()
    {
        sfxSource.loop = false;  // Stop looping the sound effect
        sfxSource.Stop();
    }


    // Reset the game over flag when restarting the level
    public void ResetGameOverFlag()
    {
        gameOverSoundPlayed = false;
    }
}
