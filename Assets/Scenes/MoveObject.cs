using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 10f; // Speed at which the object will move

    void Update()
    {
        // Get input from arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Apply movement
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
