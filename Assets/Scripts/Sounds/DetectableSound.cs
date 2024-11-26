using UnityEngine;

public class DetectableSound
{
    public readonly Vector3 sound_position;
    public readonly float sound_range;

    public DetectableSound(Vector3 position, float range)
    {
        sound_position = position;
        sound_range = range;
    }
}
