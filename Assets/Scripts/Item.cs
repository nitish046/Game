using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool AlreadyPicked = false;
	
	private void FixedUpdate()
	{
		Collider[] t = Physics.OverlapSphere(transform.position, 1.5f);
		
		//Debug.Log(t.Length);
		
		foreach (Collider a in t)
		{
			//Debug.Log(a);
			tryPick(a);
		}
	}

    private void tryPick(Collider col)
    {
        GameObject other = col.gameObject;
        if (AlreadyPicked) return;

        StackCollectables StackColl;
        if (other.TryGetComponent(out StackColl))
        {

            StackColl.AddNewItem(this.transform);

            // Use GetComponentInParent to find the Player component
            Player player = other.GetComponentInParent<Player>(); //This is necessary because the bipead is the one doing the pickups
            if (player != null)
            {
                player.PlayPickupSound();
            }

            AlreadyPicked = true;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
		
    }
}

