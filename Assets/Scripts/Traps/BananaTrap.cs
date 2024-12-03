using UnityEngine;
using System.Collections;

public abstract class Trap : MonoBehaviour
{
  // Duration for which the enemy will be immobilized
  public float effectDuration = 5f;

  // Abstract method to activate the trap
  public abstract void ActivateTrap(GameObject enemy);
}

public class BananaTrap : Trap
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Initialize the audioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Henry"))
        {
            print("Henry triggered the trap");
            audioSource.Play(); // Play the sound immediately
            ActivateTrap(other.gameObject);
            Destroy(gameObject, 2f);  // Destroy the trap after 2 seconds
        }
    }

    public override void ActivateTrap(GameObject enemy)
    {
        FamilyMember familyMemberScript = enemy.GetComponent<FamilyMember>();
        if (familyMemberScript != null)
        {
            StartCoroutine(DelayedTrapEffect(enemy));
        }
    }



     private IEnumerator DelayedTrapEffect(GameObject enemy)
    {
        audioSource.Play(); // Play the sound
        yield return new WaitForSeconds(0.3f); // Slight delay to sync with the sound

        FamilyStateMachine state_machine = enemy.gameObject.GetComponent<FamilyMember>().stateMachine;
        state_machine.freeze_state.effect_duration = effectDuration;
        state_machine.freeze_state.is_trap_slip = true;
        state_machine.ChangeState(state_machine.freeze_state);
    }
}

/*
public class BananaTrap : Trap
{
  [SerializeField] private AudioClip slipSound;  // Reference to the slip sound clip
  [SerializeField] private float volume = 1f;    // Volume for the sound

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Henry"))
    {
      PlaySlipSound();  // Play the sound effect immediately
      print("Henry triggered the trap");
      ActivateTrap(other.gameObject);
      Destroy(gameObject);  // Destroy the banana immediately
    }
  }

  public override void ActivateTrap(GameObject enemy)
  {
    FamilyMember familyMemberScript = enemy.GetComponent<FamilyMember>();
    if (familyMemberScript != null)
    {
      FamilyStateMachine state_machine = enemy.GetComponent<HenryController>().stateMachine;
      state_machine.freeze_state.effect_duration = effectDuration;
      state_machine.freeze_state.is_trap_slip = true;
      state_machine.ChangeState(state_machine.freeze_state);
    }
  }

  // Method to play the slip sound with a temporary audio GameObject

  [SerializeField] private AudioSource persistentAudioSource;
  private void PlaySlipSound()
  {
    if (slipSound != null)
    {
      // Create a temporary GameObject to play the sound
      GameObject tempAudio = new GameObject("TempSlipAudio");
      AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

      // Configure the audio properties
      tempAudioSource.clip = slipSound;
      tempAudioSource.volume = volume;
      tempAudioSource.spatialBlend = 0f;  // Set to 2D sound for consistent playback
      tempAudioSource.Play();

      // Destroy the temporary GameObject after the clip finishes playing
      Destroy(tempAudio, slipSound.length);
    }
    else
    {
      Debug.LogWarning("Slip sound clip is not assigned on the banana trap.");
    }
  }
}
*/