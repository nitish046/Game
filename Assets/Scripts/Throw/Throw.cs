using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private float speed;
    private Vector3 dir;
    private Vector3 pos;

    private bool tossed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Toss(float s, Vector3 d, Vector3 p)
    {
        speed = s; dir = d; pos = p;
        dir.Normalize();
        tossed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		if (tossed)
        {
            pos += dir * speed;
        }
	}
}
