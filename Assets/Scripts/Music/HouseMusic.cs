using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseMusic : MonoBehaviour
{
    public AudioSource mainMusicSource;           // AudioSource for main music
    public AudioSource bryanMusicSource;          // AudioSource for Bryan's room music
    public AudioSource bryanActivatedMusicSource; // AudioSource for Bryan's activated music

    private bool isPlayingBryanMusic = false;     // Tracks whether Bryan's room music is playing
    private bool isPlayingActivatedMusic = false; // Tracks whether Bryan's activated music is playing
    private bool isBryanActivated = false;        // Tracks whether Bryan is activated
    private bool isPlayerInBryansRoom = false;    // Tracks whether the player is in Bryan's room

    private Coroutine fadeOutCoroutine;
    private Coroutine fadeInCoroutine;

    private void Start()
    {
        if (mainMusicSource != null)
        {
            mainMusicSource.volume = 1f; // Ensure volume is at full
            mainMusicSource.Play();
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = false;
            Debug.Log("Starting Main Music");
        }
        else
        {
            Debug.LogWarning("Main music AudioSource is not assigned.");
        }
    }

    // Call this method to update Bryan's activation state
    public void SetBryanActivated(bool activated)
    {
        isBryanActivated = activated;
        Debug.Log("SetBryanActivated called. Activated: " + activated);

        if (activated)
        {
            PlayActivatedMusic();
        }
        else
        {
            if (isPlayerInBryansRoom)
            {
                PlayBryanRoomMusic();
            }
            else
            {
                PlayMainMusic();
            }
        }
    }

    // Call when the player enters Bryan's room
    public void OnEnterBryansRoom()
    {
        isPlayerInBryansRoom = true;
        Debug.Log("OnEnterBryansRoom called. isBryanActivated: " + isBryanActivated);

        if (isBryanActivated)
        {
            PlayActivatedMusic();
        }
        else
        {
            PlayBryanRoomMusic();
        }
    }

    // Call when the player exits Bryan's room
    public void OnExitBryansRoom()
    {
        isPlayerInBryansRoom = false;
        Debug.Log("OnExitBryansRoom called. isBryanActivated: " + isBryanActivated);

        PlayMainMusic();
    }

    private void PlayBryanRoomMusic()
    {
        if (!isPlayingBryanMusic && bryanMusicSource != null)
        {
            Debug.Log("Switching to Bryan Room Music");
            StopFadeCoroutines();
            StartCoroutine(FadeOutCurrentMusicAndFadeIn(bryanMusicSource));
            isPlayingBryanMusic = true;
            isPlayingActivatedMusic = false;
        }
        else if (isPlayingBryanMusic)
        {
            Debug.Log("Bryan Room Music is already playing");
        }
        else
        {
            Debug.LogWarning("Bryan Room Music AudioSource is not assigned.");
        }
    }

    private void PlayActivatedMusic()
    {
        if (!isPlayingActivatedMusic && bryanActivatedMusicSource != null)
        {
            Debug.Log("Switching to Bryan Activated Music");
            StopFadeCoroutines();
            StartCoroutine(FadeOutCurrentMusicAndFadeIn(bryanActivatedMusicSource));
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = true;
        }
        else if (isPlayingActivatedMusic)
        {
            Debug.Log("Bryan Activated Music is already playing");
        }
        else
        {
            Debug.LogWarning("Bryan Activated Music AudioSource is not assigned.");
        }
    }

    private void PlayMainMusic()
    {
        if ((isPlayingBryanMusic || isPlayingActivatedMusic) && mainMusicSource != null)
        {
            Debug.Log("Switching to Main Music");
            StopFadeCoroutines();
            StartCoroutine(FadeOutCurrentMusicAndFadeIn(mainMusicSource));
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = false;
        }
        else if (!isPlayingBryanMusic && !isPlayingActivatedMusic)
        {
            Debug.Log("Main Music is already playing");
        }
        else
        {
            Debug.LogWarning("Main Music AudioSource is not assigned.");
        }
    }

    // Fades out the currently playing music and fades in the new music
    private IEnumerator FadeOutCurrentMusicAndFadeIn(AudioSource newMusic)
    {
        float fadeDuration = 1f; // Duration for fade out and fade in

        // Fade out current music
        List<AudioSource> playingSources = new List<AudioSource>();
        if (mainMusicSource != null && mainMusicSource.isPlaying && mainMusicSource != newMusic)
        {
            playingSources.Add(mainMusicSource);
        }
        if (bryanMusicSource != null && bryanMusicSource.isPlaying && bryanMusicSource != newMusic)
        {
            playingSources.Add(bryanMusicSource);
        }
        if (bryanActivatedMusicSource != null && bryanActivatedMusicSource.isPlaying && bryanActivatedMusicSource != newMusic)
        {
            playingSources.Add(bryanActivatedMusicSource);
        }

        // Start fade out for current music
        foreach (AudioSource source in playingSources)
        {
            fadeOutCoroutine = StartCoroutine(FadeOut(source, fadeDuration));
        }

        // Wait for fade out to complete
        yield return new WaitForSeconds(fadeDuration);

        // Fade in new music
        fadeInCoroutine = StartCoroutine(FadeIn(newMusic, fadeDuration));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        if (audioSource == null)
            yield break;

        float startVolume = audioSource.volume;

        float time = 0;
        while (audioSource.volume > 0)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        if (audioSource == null)
            yield break;

        float targetVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        float time = 0;
        while (audioSource.volume < targetVolume)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private void StopFadeCoroutines()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = null;
        }
    }
}
