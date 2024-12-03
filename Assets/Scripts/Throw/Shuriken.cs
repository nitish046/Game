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

        

        if (collision.gameObject.CompareTag("Henry"))
        {
            FamilyStateMachine state_machine = collision.gameObject.GetComponent<FamilyMember>().stateMachine;
            state_machine.freeze_state.effect_duration = 5f;
            state_machine.freeze_state.is_trap_slip = true;
            state_machine.ChangeState(state_machine.freeze_state);
        }
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
            if(o.layer == 7 || o.CompareTag("Henry"))
            {
                Vector3 n = new Vector3(hit.normal.x, 0, hit.normal.z);
                n.Normalize();
                v = Vector3.Reflect(v, n);
                if (++bounces >= 3)
                    Destroy(this.gameObject);
                return;
            }
        }
    }
}
