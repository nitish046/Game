using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableItem : Item
{
  [SerializeField] private StackCollectables stackCollectables;
  [SerializeField] private Image itemImage;
  private string ItemName;

  public string getItemName()
  {
    return ItemName;
  }

  public Image getItemImage()
  {
    return itemImage;
  }


  protected override void tryPick(Collider col)
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

        stackCollectables.AddItemToHotbar(this);

        already_picked = true;
      }
    }
  }
}
