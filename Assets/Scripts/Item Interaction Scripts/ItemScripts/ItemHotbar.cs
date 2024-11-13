using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Numerics;
using UnityEngine.Animations;
using System;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Text = TMPro.TextMeshProUGUI;
using TMPro;

public class ItemHotbar : MonoBehaviour
{
  private List<UsableItem> itemList = new List<UsableItem>();
  private List<int> itemCount = new List<int>();

  [SerializeField] private int numBoxes = 8;
  [SerializeField] private List<Image> hotbarBoxes;
  [SerializeField] private List<Image> hotbarImages;
  [SerializeField] private List<GameObject> hotbarCounts;
  [SerializeField] private Sprite unselectedBox;
  [SerializeField] private Sprite selectedBox;

  private int currentIndex = 0;

  public GameInput gameInput;
  private InputAction toggleLeft;
  private InputAction toggleRight;
  private InputAction toggle;

  private void Start()
  {
    StartCoroutine(this.DrawHotbarCoroutine());
    hotbarBoxes[0].sprite = selectedBox;
    for (int i = 1; i < numBoxes; i++)
    {
      hotbarBoxes[i].sprite = unselectedBox;
    }

  }
  private void OnEnable()
  {
    // toggle = gameInput.player_input_actions.Player.ToggleHotbar;
    // toggle.Enable();
  }
  private void OnDisable()
  {
    // toggle.Disable();
  }

  private void Update()
  {
    // toggleHotbarValue(toggle.ReadValue<Axis>());
    toggleHotbarValue();
  }

  private void toggleHotbarValue(/*Axis toggleAxis*/)
  {
    // Debug.Log("currentIndex: " + currentIndex);
    hotbarBoxes[currentIndex].sprite = unselectedBox;
    // int hotbarToggle = (toggleAxis == 0) ? 0 : (int)((float)toggleAxis / Math.Abs((float)toggleAxis));
    // int hotbarToggle = (toggleAxis == 0) ? 0 : ((toggleAxis < 0) ? -1 : 1);
    // int hotbarToggle = gameInput.getHotbarInput();
    int hotbarToggle = 0;
    if (Input.GetKeyDown(KeyCode.Q))
    {
      hotbarToggle = -1;
    }
    else if (Input.GetKeyDown(KeyCode.E))
    {
      hotbarToggle = 1;
    }
    // Debug.Log("hotbarToggle: " + hotbarToggle);
    currentIndex += hotbarToggle;
    // Debug.Log("new currentIndex: " + currentIndex);
    if (currentIndex >= numBoxes)
    {
      currentIndex = numBoxes - 1;
    }
    else if (currentIndex < 0)
    {
      currentIndex = 0;
    }

    if (currentIndex < numBoxes)
    {
      hotbarBoxes[currentIndex].sprite = selectedBox;
    }
    else
    {
      hotbarBoxes[numBoxes - 1].sprite = selectedBox;
    }
  }

  public IEnumerator DrawHotbarCoroutine()
  {
    float delay = 0.2f;
    WaitForSeconds wait = new WaitForSeconds(delay);
    while (true)
    {
      yield return wait;
      DrawHotbar();
    }
  }

  public void DrawHotbar()
  {
    // Debug.Log("drawing hotbar");
    if (itemList.Count <= numBoxes || currentIndex < numBoxes)
    {
      // Debug.Log("itemList.Count <= numBoxes || currentIndex < numBoxes");
      for (int i = 0; i < numBoxes; i++)
      {
        // Debug.Log("i = " + i);
        // Debug.Log("itemList.Count = " + itemList.Count);
        if (i < itemList.Count)
        {
          // Debug.Log("i < itemList.Count");
          hotbarImages[i].sprite = itemList[i].getItemSprite();
          hotbarImages[i].color = Color.white;
          // Debug.Log("itemCount " + itemCount[i].ToString());
          // Debug.Log("hotbarcount " + hotbarCounts[i]);
          // Debug.Log("hotbarcount " + hotbarCounts[i].GetComponentAtIndex(2));
          // Debug.Log("hotbarcount " + hotbarCounts[i].GetComponent<Text>());
          // Debug.Log("hotbarcount " + hotbarCounts[i].GetComponent<Text>().text);
          hotbarCounts[i].GetComponent<Text>().text = itemCount[i].ToString();
          // Debug.Log(hotbarImages[i].sprite.ToString());
          // Debug.Log(itemList[i].ToString());
          // Debug.Log(itemList[i].getItemSprite().ToString());
        }
        else
        {
          // Debug.Log("NOT i < itemList.Count");
          hotbarImages[i].color = Color.clear;
        }
      }
      // Debug.Log("EXITING itemList.Count <= numBoxes || currentIndex < numBoxes");
    }
    else
    {
      // Debug.Log("NOT itemList.Count <= numBoxes || currentIndex < numBoxes");
      int min_i = currentIndex - numBoxes;
      for (int i = min_i; i < numBoxes; i++)
      {
        // Debug.Log("i = " + i);
        hotbarBoxes[i - min_i].sprite = itemList[i].getItemSprite();
        hotbarImages[i - min_i].color = Color.white;
        hotbarCounts[i - min_i].GetComponent<Text>().text = itemCount[i].ToString();
        // Debug.Log("itemCount" + itemCount[i - min_i].ToString());
        // Debug.Log("hotbarcount" + hotbarCounts[i - min_i].GetComponent<Text>().text);
      }
    }

  }

  public void AddItem(UsableItem uItem)
  {
    // Debug.Log("Adding Item");
    for (int i = 0; i < itemList.Count; i++)
    {
      // Debug.Log("i: " + i);
      // Debug.Log("itemList[i].getItemName(): " + itemList[i].getItemName() + "uItem.getItemName(): " + uItem.getItemName());
      if (itemList[i].getItemName() == uItem.getItemName())
      {
        // Debug.Log("itemList[i].getItemName() == uItem.getItemName()");
        itemCount[i]++;
        return;
      }
    }
    // Debug.Log("Adding " + uItem.getItemName());
    itemList.Add(uItem);
    // Debug.Log(itemList.ToString());
    itemCount.Add(1);
    // Debug.Log(itemCount.ToString());
    // hotbarImages.Add(uItem.getItemImage());
  }
}
