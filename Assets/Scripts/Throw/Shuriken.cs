using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Throw
{
    private int bounces = 0;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    public void Toss(float s, Transform t)
    {
        base.Toss(s, t);
    }

	private void OnCollisionEnter(Collision collision)
	{
		bounces++;
        if (bounces >= 3)
            Destroy(this);
        v = Vector3.Reflect(v, collision.GetContact(0).normal);
	}

    protected void FixedUpdate()
    {
        base.FixedUpdate();
        CheckBounce(rb.SweepTestAll(v, v.magnitude * Time.fixedDeltaTime));
    }

    private void CheckBounce(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            GameObject o = hit.collider.gameObject;
            if(o.layer == 7 && o.CompareTag("Untagged"))
            {
                v = Vector3.Reflect(v, hit.normal);
                if (++bounces >= 3)
                    Destroy(this.gameObject);
                return;
            }
        }
    }
}
