using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public float effectDuration = 5f;
    [SerializeField] private AudioClip splatSoundClip;  // Reference to the Audio Clip for the splat sound
    [SerializeField] private float volume = 1f;         // Set volume for the splat sound

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Henry"))
        {
            PlaySplatSound();

            if (collision.gameObject.CompareTag("Henry"))
            {
                // Call Freeze with effectDuration and isTrapFreeze = false (since this isn't a trap)
                FamilyStateMachine state_machine = collision.gameObject.GetComponent<FamilyMember>().stateMachine;
                state_machine.freeze_state.effect_duration = effectDuration;
                state_machine.freeze_state.is_trap_slip = false;
                state_machine.ChangeState(state_machine.freeze_state);
            }

            Destroy(gameObject);  // Destroy immediately with no delay
        }
    }

    private void PlaySplatSound()
    {
        if (splatSoundClip != null)
        {
            // Create a new GameObject to play the splat sound independently
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

            // Configure the temporary AudioSource
            tempAudioSource.clip = splatSoundClip;
            tempAudioSource.volume = volume;
            tempAudioSource.spatialBlend = 0f;
            tempAudioSource.Play();

            // Destroy the temporary GameObject after the clip finishes playing
            Destroy(tempAudio, splatSoundClip.length);
        }
        else
        {
            Debug.LogWarning("Splat sound clip is not assigned.");
        }
    }
}
