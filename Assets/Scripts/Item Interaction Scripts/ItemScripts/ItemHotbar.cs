using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Numerics;
using UnityEngine.Animations;
using System;
using UnityEngine.UI;

public class ItemHotbar : MonoBehaviour
{
  private List<UsableItem> itemList = new List<UsableItem>();
  private List<int> itemCount = new List<int>();

  [SerializeField] private int numBoxes = 8;
  [SerializeField] private List<Image> hotbarBoxes;
  [SerializeField] private List<Image> hotbarImages;
  [SerializeField] private Sprite unselectedBox;
  [SerializeField] private Sprite selectedBox;

  private int currentIndex = 0;

  public PlayerInputActions playerInputAction;
  private InputAction toggleLeft;
  private InputAction toggleRight;
  private InputAction toggle;

  private void Start()
  {
    StartCoroutine(this.DrawHotbar());
    hotbarBoxes[0].sprite = selectedBox;
    for (int i = 1; i < numBoxes; i++)
    {
      hotbarBoxes[i].sprite = unselectedBox;
    }

  }
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
    toggleHotbarValue(toggle.ReadValue<Axis>());
  }

  private void toggleHotbarValue(Axis toggleAxis)
  {
    hotbarBoxes[currentIndex].sprite = unselectedBox;
    // int hotbarToggle = (toggleAxis == 0) ? 0 : (int)((float)toggleAxis / Math.Abs((float)toggleAxis));
    int hotbarToggle = (toggleAxis == 0) ? 0 : ((toggleAxis < 0) ? -1 : 1);
    currentIndex += hotbarToggle;
    if (currentIndex >= itemList.Count)
      currentIndex = itemList.Count - 1;
    else if (currentIndex < 0)
      currentIndex = 0;

    if (currentIndex < numBoxes)
    {
      hotbarBoxes[currentIndex].sprite = selectedBox;
    }
    else
    {
      hotbarBoxes[numBoxes - 1].sprite = selectedBox;
    }
  }

  public IEnumerator DrawHotbar()
  {
    float delay = 0.2f;
    WaitForSeconds wait = new WaitForSeconds(delay);
    while (true)
    {
      yield return wait;
      if (itemList.Count <= numBoxes || currentIndex < numBoxes)
      {
        for (int i = 0; i < numBoxes; i++)
        {
          if (i < itemList.Count)
          {
            hotbarImages[i] = itemList[i].getItemImage();
            hotbarImages[i].color = Color.white;
          }
          else
          {
            hotbarImages[i].color = Color.clear;
          }
        }
      }
      else
      {
        int min_i = currentIndex - numBoxes;
        for (int i = min_i; i < numBoxes; i++)
        {
          hotbarBoxes[i - min_i] = itemList[i].getItemImage();
          hotbarImages[i - min_i].color = Color.white;
        }
      }
    }
  }

  public void AddItem(UsableItem uItem)
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
    // hotbarImages.Add(uItem.getItemImage());
  }
}
