using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	bool already_picked = false;

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
		//Debug.Log(other);
		if (already_picked) return;
		//if (other.CompareTag("Player"))
		{
			//Debug.Log("TUCH");	
			StackCollectables StackColl;
			if (other.TryGetComponent(out StackColl))
			{
				Debug.Log("Pick");
				StackColl.AddNewItem(this.transform);

				// Use GetComponentInParent to find the Player component
				Player player = other.GetComponentInParent<Player>(); //This is necessary because the bipead is the one doing the pickups
				if (player != null)
				{
					player.PlayPickupSound();
				}

				already_picked = true;
			}
		}
	}
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{

	}
}

