using UnityEngine;

public class MoveObject : MonoBehaviour
{
	float Deg = 180/Mathf.PI;
	
    public float speed;
	public float jumpHeight;
	public float gravity;
	public float rotationSpeed;
	private bool grounded;
	private Vector3 velocity;
	
	public GameObject camera; 
	
	public CharacterController c;
	float horizontal, vertical;
	
	float azimuth;
	Quaternion rotation;
	
	Vector3 facing;
	
	void Start()
	{
		c = GetComponent<CharacterController>();
	}

    void Update()
    {
        // Get input from arrow keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
		
		Vector3 f = camera.transform.forward;
		f.y = 0;
		Vector3 r = Vector3.Cross(Vector3.up, f);
		
		Vector3 offset = (f * vertical + r * horizontal);
		//rb.MovePosition(transform.position + offset * accel * Time.deltaTime);
		//rb.AddForce(offset * accel * Time.deltaTime, ForceMode.VelocityChange);
		
		if(offset.magnitude != 0)
		{
			if(offset.magnitude > 1)
				offset.Normalize();
			transform.LookAt(Vector3.Slerp(transform.position + transform.forward, transform.position + offset, rotationSpeed));
			Vector3 movement = offset * speed * Time.deltaTime;
			c.Move(offset);
			
		}
		
        // Calculate movement direction
        // Apply movement
		
        //transform.Translate(movement * speed * Time.deltaTime, Space.World);
		
		//Debug.Log(rb.velocity.ToString());
    }
	
	void FixedUpdate()
	{
		
	}
	
	void LateUpdate()
	{
	}
}
