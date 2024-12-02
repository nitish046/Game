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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Toss(float s, Vector3 d)
    {
        speed = s; dir = d;
        dir.Normalize();
        rb.velocity = dir * speed;
        tossed = true;
    }

	public void Toss(float s, Quaternion r)
	{
		speed = s; rot = r;
		dir.Normalize();
		rb.velocity = (rot * Vector3.forward) * speed;
		tossed = true;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void FixedUpdate()
	{

	}
}
