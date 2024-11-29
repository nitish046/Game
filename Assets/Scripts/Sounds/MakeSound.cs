using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    private static LayerMask mask = LayerMask.GetMask("Default"); 
    public static void GetSoundListners(DetectableSound sound)
    {
        Collider[] col = Physics.OverlapSphere(sound.sound_position, sound.sound_range, mask);

        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].TryGetComponent(out FamilyMember listener))
            {
                listener.RespondToSound(sound);
            }
            else if(col[i].TryGetComponent(out BryanController bryan_listener))
            {
                bryan_listener.RespondToSteal();
            }
        }
    }
}
