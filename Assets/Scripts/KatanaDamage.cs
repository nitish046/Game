using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the player's health component
            LifeTracker player_health = other.GetComponent<LifeTracker>();

            if (player_health != null)
            {
                player_health.LoseLife();
            }
        }
    }
}
