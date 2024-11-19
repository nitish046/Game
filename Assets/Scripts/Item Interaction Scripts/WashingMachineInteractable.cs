using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineInteractable : Interactable
{
    private AudioSource audio_source;
    private float sound_range = 25f;

    protected override void Interact()
    {
        audio_source = GetComponent<AudioSource>();
        if (audio_source == null || audio_source.isPlaying)
        {
            return;
        }

        audio_source.Play();

        var sound = new DetectableSound(transform.position, sound_range);
        MakeSound.GetSoundListners(sound);
    }
}
