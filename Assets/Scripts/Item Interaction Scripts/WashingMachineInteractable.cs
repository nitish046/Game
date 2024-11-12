using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineInteractable : Interactable
{
    private AudioSource audioSource;

    protected override void Interact()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
