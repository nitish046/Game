using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Throw
{
    private int hits = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		hits++;
        if (hits >= 3)
            Destroy(this);
        rb.velocity = Vector3.Reflect(rb.velocity, collision.GetContact(0).normal);
	}
}
