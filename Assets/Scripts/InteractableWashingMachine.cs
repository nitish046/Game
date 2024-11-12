using UnityEngine;

public class InteractableWashingMachine : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Reference to the washing machine sound
    [SerializeField] private float interactionDistance = 3f;  // Distance within which player can interact
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource component missing on the washing machine prefab.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player is in range of the washing machine. Press 'E' to start.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player is out of range of the washing machine.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayWashingMachineSound();
        }
    }

    private void PlayWashingMachineSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Washing machine sound playing.");
        }
    }
}
