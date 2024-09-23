using UnityEngine;

public class TankController : MonoBehaviour
{
    public float movementSpeed = 5;
    public float rotationSpeed = 50;
    public float cameraDistance = 5;

    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void FixedUpdate()
    {
		float factor = Input.GetAxis("Vertical");
		
		if(factor < 0)
			factor /= 2.0f;
		
        transform.position += transform.rotation * new Vector3(0, 0, factor) * movementSpeed * Time.fixedDeltaTime;
		
		transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime * factor, 0);

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, 1, -cameraDistance);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}