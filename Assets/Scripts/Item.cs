using System.Collections;
using System.Collections.Generic;
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
		//Debug.Log(other);
		if (AlreadyPicked) return;
        //if (other.CompareTag("Player"))
        {
            //Debug.Log("TUCH");  
            StackCollectables StackColl;
            if(other.TryGetComponent(out StackColl))
            {
				Debug.Log("Pick");
                StackColl.AddNewItem(this.transform);

                AlreadyPicked = true;
            }
        }
	}
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
		
    }
}

