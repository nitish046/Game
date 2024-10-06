using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool AlrudyPicked = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {if (AlrudyPicked) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("TUCH");  
            StackCollectables StackColl;
            if(other.TryGetComponent(out StackColl))
            {
                StackColl.AddNewItem(this.transform);
                AlrudyPicked = true;
            }
        }
    }
}

