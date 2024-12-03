using UnityEngine;

public class HouseMusic : MonoBehaviour
{
    public AudioSource mainMusicSource;   // AudioSource for main music
    public AudioSource bryanMusicSource;  // AudioSource for Bryan's music

    private bool isPlayingBryanMusic = false; // Tracks whether Bryan's music is playing

    private void Start()
    {
        if (mainMusicSource != null)
        {
            mainMusicSource.Play();
            isPlayingBryanMusic = false;
            Debug.Log("Starting Main Music");
        }
        else
        {
            Debug.LogWarning("Main music AudioSource is not assigned.");
        }
    }

    public void PlayBryanRoomMusic()
    {
        if (!isPlayingBryanMusic && bryanMusicSource != null)
        {
            Debug.Log("Switching to Bryan Room Music");
            if (mainMusicSource != null && mainMusicSource.isPlaying)
            {
                mainMusicSource.Stop();
            }
            bryanMusicSource.Play();
            isPlayingBryanMusic = true;
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

    public void PlayMainMusic()
    {
        if (isPlayingBryanMusic && mainMusicSource != null)
        {
            Debug.Log("Switching to Main Music");
            if (bryanMusicSource != null && bryanMusicSource.isPlaying)
            {
                bryanMusicSource.Stop();
            }
            mainMusicSource.Play();
            isPlayingBryanMusic = false;
        }
        else if (!isPlayingBryanMusic)
        {
            Debug.Log("Main Music is already playing");
        }
        else
        {
            Debug.LogWarning("Main Music AudioSource is not assigned.");
        }
    }
}
