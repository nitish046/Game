using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseMusic : MonoBehaviour
{
    public AudioSource mainMusicSource;                // AudioSource for main music
    public AudioSource bryanMusicSource;               // AudioSource for Bryan's room music
    public AudioSource bryanActivatedMusicSource;      // AudioSource for Bryan's activated music
    public AudioSource familyActivatedMusicSource;     // AudioSource for family members' activated music

    private bool isPlayingBryanMusic = false;          // Tracks whether Bryan's room music is playing
    private bool isPlayingActivatedMusic = false;      // Tracks whether Bryan's activated music is playing
    private bool isBryanActivated = false;             // Tracks whether Bryan is activated
    private bool isPlayerInBryansRoom = false;         // Tracks whether the player is in Bryan's room

    private int familyMembersActivated = 0;            // Tracks the number of family members activated
    private Coroutine familyMusicDelayCoroutine = null; // Coroutine for family music delay
    private bool isPlayingFamilyActivatedMusic = false; // Tracks whether family activated music is playing

    private Coroutine fadeOutCoroutine = null;
    private Coroutine fadeInCoroutine = null;

    private void Start()
    {
        if (mainMusicSource != null)
        {
            mainMusicSource.volume = 1f; // Ensure volume is at full
            mainMusicSource.Play();
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = false;
            isPlayingFamilyActivatedMusic = false;
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
        Debug.Log("SetBryanActivated called. Bryan Activated: " + activated);

        UpdateMusic();
    }

    // Call this method to update family members' activation state
    public void SetFamilyMemberActivated(bool activated)
    {
        if (activated)
        {
            if (familyMembersActivated == 0)
            {
                // First family member activated
                familyMembersActivated++;
                Debug.Log("Family member activated. Total activated: " + familyMembersActivated);
                if (familyMusicDelayCoroutine != null)
                {
                    // Cancel any pending delay coroutine
                    StopCoroutine(familyMusicDelayCoroutine);
                    familyMusicDelayCoroutine = null;
                }
                UpdateMusic();
            }
            else
            {
                familyMembersActivated++;
                Debug.Log("Another family member activated. Total activated: " + familyMembersActivated);
                // Do not restart music if it's already playing
            }
        }
        else
        {
            familyMembersActivated = Mathf.Max(0, familyMembersActivated - 1);
            Debug.Log("Family member deactivated. Total activated: " + familyMembersActivated);
            if (familyMembersActivated == 0)
            {
                // All family members deactivated, start delay before switching music
                if (familyMusicDelayCoroutine != null)
                {
                    StopCoroutine(familyMusicDelayCoroutine);
                }
                familyMusicDelayCoroutine = StartCoroutine(WaitAndSwitchToMainMusic(5f)); // 5-second delay
            }
        }
    }

    private IEnumerator WaitAndSwitchToMainMusic(float delay)
    {
        Debug.Log("Starting delay of " + delay + " seconds before switching to main music.");
        yield return new WaitForSeconds(delay);
        familyMusicDelayCoroutine = null;
        Debug.Log("Delay complete. Switching music if no family members are activated.");
        if (familyMembersActivated == 0)
        {
            UpdateMusic();
        }
    }

    // Call when the player enters Bryan's room
    public void OnEnterBryansRoom()
    {
        isPlayerInBryansRoom = true;
        Debug.Log("OnEnterBryansRoom called. isBryanActivated: " + isBryanActivated);

        UpdateMusic();
    }

    // Call when the player exits Bryan's room
    public void OnExitBryansRoom()
    {
        isPlayerInBryansRoom = false;
        Debug.Log("OnExitBryansRoom called. isBryanActivated: " + isBryanActivated);

        UpdateMusic();
    }

    // Determines which music should play based on the current state
    private void UpdateMusic()
    {
        // Priority: Bryan Activated Music > Family Activated Music > Bryan Room Music > Main Music

        // If Bryan is activated and the player is in his room
        if (isBryanActivated && isPlayerInBryansRoom)
        {
            PlayBryanActivatedMusic();
        }
        // Else if any family member is activated
        else if (familyMembersActivated > 0)
        {
            PlayFamilyActivatedMusic();
        }
        // Else if the player is in Bryan's room
        else if (isPlayerInBryansRoom)
        {
            PlayBryanRoomMusic();
        }
        // Default to main music
        else
        {
            PlayMainMusic();
        }
    }

    private void PlayBryanRoomMusic()
    {
        if (!isPlayingBryanMusic && bryanMusicSource != null)
        {
            Debug.Log("Switching to Bryan Room Music");
            StopFadeCoroutines();
            StartFadeTransition(bryanMusicSource);
            isPlayingBryanMusic = true;
            isPlayingActivatedMusic = false;
            isPlayingFamilyActivatedMusic = false;
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

    private void PlayBryanActivatedMusic()
    {
        if (!isPlayingActivatedMusic && bryanActivatedMusicSource != null)
        {
            Debug.Log("Switching to Bryan Activated Music");
            StopFadeCoroutines();
            StartFadeTransition(bryanActivatedMusicSource);
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = true;
            isPlayingFamilyActivatedMusic = false;
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

    private void PlayFamilyActivatedMusic()
    {
        if (!isPlayingFamilyActivatedMusic && familyActivatedMusicSource != null)
        {
            Debug.Log("Switching to Family Activated Music");
            StopFadeCoroutines();
            StartFadeTransition(familyActivatedMusicSource);
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = false;
            isPlayingFamilyActivatedMusic = true;
        }
        else if (isPlayingFamilyActivatedMusic)
        {
            Debug.Log("Family Activated Music is already playing");
        }
        else
        {
            Debug.LogWarning("Family Activated Music AudioSource is not assigned.");
        }
    }

    private void PlayMainMusic()
    {
        if ((isPlayingBryanMusic || isPlayingActivatedMusic || isPlayingFamilyActivatedMusic) && mainMusicSource != null)
        {
            Debug.Log("Switching to Main Music");
            StopFadeCoroutines();
            StartFadeTransition(mainMusicSource);
            isPlayingBryanMusic = false;
            isPlayingActivatedMusic = false;
            isPlayingFamilyActivatedMusic = false;
        }
        else if (mainMusicSource.isPlaying)
        {
            Debug.Log("Main Music is already playing");
        }
        else
        {
            Debug.LogWarning("Main Music AudioSource is not assigned.");
        }
    }

    // Starts the fade out of current music and fade in of new music
    private void StartFadeTransition(AudioSource newMusic)
    {
        StopAllCoroutinesExceptFamilyDelay();
        fadeOutCoroutine = StartCoroutine(FadeOutCurrentMusicAndFadeIn(newMusic));
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
        if (familyActivatedMusicSource != null && familyActivatedMusicSource.isPlaying && familyActivatedMusicSource != newMusic)
        {
            playingSources.Add(familyActivatedMusicSource);
        }

        // Start fade out for current music
        foreach (AudioSource source in playingSources)
        {
            StartCoroutine(FadeOut(source, fadeDuration));
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

    private void StopAllCoroutinesExceptFamilyDelay()
    {
        // Stop all coroutines except the family music delay coroutine
        StopFadeCoroutines();
        // Do not stop familyMusicDelayCoroutine
    }
}
