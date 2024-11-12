using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTool : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        Transform hit_transform = collision.transform;
        if(hit_transform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hit_transform.GetComponentInParent<LifeTracker>().LoseLife();
        }
        Destroy(gameObject);
    }
}
