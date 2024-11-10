using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHotbar : MonoBehaviour
{
  private List<UsableItem> itemList = new List<UsableItem>();
  private List<int> itemCount = new List<int>();

  public void DrawHotbar()
  {

  }

  public void AddItemToHotbar(UsableItem uItem)
  {
    for (int i = 0; i < itemList.Count; i++)
    {
      if (itemList[i].name == uItem.name)
      {
        itemCount[i]++;
        return;
      }
    }
    itemList.Add(uItem);
    itemCount.Add(1);
  }
}
