using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  protected bool already_picked = false;

  [SerializeField] protected int itemPoints;

  protected void FixedUpdate()
  {
    Collider[] t = Physics.OverlapSphere(transform.position, 1.5f);

    //Debug.Log(t.Length);

    foreach (Collider a in t)
    {
      //Debug.Log(a);
      tryPick(a);
    }
  }

  protected virtual void tryPick(Collider col)
  {
    GameObject other = col.gameObject;
    //Debug.Log(other);
    if (already_picked) return;
    //if (other.CompareTag("Player"))
    //{
    //Debug.Log("TUCH");	
    StackCollectables StackColl;
    if (tryPickItemCheck(other, out StackColl))
    {
      // Debug.Log("this, " + base.name + " other: " + other.name);
      // Debug.Log("Pick");
      // Debug.Log("calling AddNewItem from Item");
      StackColl.AddNewItem(this.transform, itemPoints);

      // Use GetComponentInParent to find the Player component
      Player player = other.GetComponentInParent<Player>(); //This is necessary because the bipead is the one doing the pickups
      if (player != null)
      {
        player.PlayPickupSound();
      }

      already_picked = true;
    }
    //}
  }

  protected bool tryPickItemCheck(GameObject other, out StackCollectables StackColl)
  {
    Transform thisTransform = base.transform;
    // if (other.name == "player")
    // {
    //   Debug.Log("this, " + base.name + " other: " + other);
    //   Debug.Log(thisTransform.position.y + " " + other.transform.position.y + 1);
    // }
    return (other.TryGetComponent(out StackColl) && thisTransform.position.y < other.transform.position.y + 1);
  }

  // Start is called before the first frame update
  protected void OnTriggerEnter(Collider other)
  {

  }
}

