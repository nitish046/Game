using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UsableItem : Item
{
  [SerializeField] private Sprite itemSprite;
  private string ItemName;

  public string getItemName()
  {
    return ItemName;
  }

  public Sprite getItemSprite()
  {
    return itemSprite;
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
        // Debug.Log("Pick");
        // Debug.Log("Calling AddNewItem from UsableItem");
        StackColl.AddNewItem(this.transform, itemPoints);

        // Use GetComponentInParent to find the Player component
        Player player = other.GetComponentInParent<Player>(); //This is necessary because the bipead is the one doing the pickups
        if (player != null)
        {
          player.PlayPickupSound();
        }

        already_picked = true;

        StackColl.AddItemToHotbar(this);
      }
    }
  }
}
