using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Numerics;
using UnityEngine.Animations;
using System;

public class ItemHotbar : MonoBehaviour
{
  private List<UsableItem> itemList = new List<UsableItem>();
  private List<int> itemCount = new List<int>();

  private int currentIndex = 0;

  private PlayerInputActions playerInputAction;
  private InputAction toggleLeft;
  private InputAction toggleRight;
  private InputAction toggle;

  private void OnEnable()
  {
    toggle = playerInputAction.Player.ToggleHotbar;
    toggle.Enable();
  }
  private void OnDisable()
  {
    toggle.Disable();
  }

  private void Update()
  {
    int hotbarToggle = (toggle.ReadValue<Axis>() == 0) ? 0 : (int)((float)toggle.ReadValue<Axis>() / Math.Abs((float)toggle.ReadValue<Axis>()));
    currentIndex += hotbarToggle;
    if (currentIndex >= itemList.Count) currentIndex = 0;
    if (currentIndex < 0) currentIndex = itemList.Count - 1;
  }

  public void DrawHotbar()
  {
    if (itemList.Count == 0)
    {

    }
    else
    {

    }
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
