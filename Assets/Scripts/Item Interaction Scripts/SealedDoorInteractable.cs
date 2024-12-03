using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedDoorInteractable : Interactable
{
    private AudioSource door_audio;

    private void Awake()
    {
        door_audio = GetComponent<AudioSource>();
    }

    protected override void Interact()
    {
        prompt_message = "I Can't\nOpen This";

        if (door_audio == null || door_audio.isPlaying)
        {
            return;
        }

        door_audio.Play();
    }
}
