using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private float speed;
    private Vector3 dir;
    private Quaternion rot;

    protected Rigidbody rb;

    private bool tossed = false;

    protected Vector3 v;

    // Start is called before the first frame update
    protected void Start()
    {

    }

    public void Toss(float s, Transform t)
    {
        speed = s;
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.isKinematic = true;
        transform.position = t.position;
        v = t.forward * s;
        tossed = true;
    }

	// Update is called once per frame
	protected void Update()
    {
        //Debug.Log(rb.velocity);
    }

	protected void FixedUpdate()
	{
        transform.position += v * Time.fixedDeltaTime;
	}
}
