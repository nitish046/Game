using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StackCollectables : MonoBehaviour
{
    // Height offset to stack collectables
    public float stackHeight = 1.0f;

    // Track the number of stacked collectables
    private int stackCount = 0;

    private void Update()
    {
        //OnCollisionEnter()
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        // Check if the collided object has the tag "Collectables"
        if (collision.gameObject.CompareTag("Collectables"))
        {
            // Make the collectable object a child of this GameObject
            Debug.Log("Collectable");
            collision.transform.SetParent(transform);

            // Calculate the new position for the collectable to stack on top
            Vector3 stackPosition = transform.position + Vector3.up * stackHeight * (stackCount + 1);

            // Set the new position for the collectable
            collision.transform.localPosition = new Vector3(0, stackHeight * (stackCount + 1), 0);

            // Increment the stack count
            stackCount++;
        }
    }

}
