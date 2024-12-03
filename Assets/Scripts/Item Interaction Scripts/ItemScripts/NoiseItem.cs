using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseItem : Item
{
    private AudioSource audio_source;
    public float sound_range = 25f;

    protected override void tryPick(Collider col)
    {
        GameObject other = col.gameObject;
        if (already_picked) return;
        StackCollectables StackColl;
        if (tryPickItemCheck(other, out StackColl))
        {
            StackColl.AddNewItem(this.transform, itemPoints);
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.PlayPickupSound();
            }

            already_picked = true;
            PlayNoise();
        }
    }

    protected void PlayNoise()
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
