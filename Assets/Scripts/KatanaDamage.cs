using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaDamage : MonoBehaviour
{
    private bool can_damage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            LifeTracker player_health = other.GetComponent<LifeTracker>();

            if (player_health != null && can_damage)
            {
                player_health.LoseLife();
            }
        }
    }

    public void SetCanDamage(bool can)
    {
        can_damage = can;
    }

}
