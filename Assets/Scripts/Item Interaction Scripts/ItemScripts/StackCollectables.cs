using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StackCollectables : MonoBehaviour
{
  public TMP_Text score_text;
  public TMP_Text win_text;

  // public GameObject winScreen;
  // public GameObject mainScreen;
  // public Button restart_button;
  // public Button quit_button;

  public Transform item_holder;
  public int current_num_points;
  public int num_points_to_win;
  public float y_position;

  public GameObject hotbar;

  [SerializeField] protected GameObject win_lose_controller;

  private void Start()
  {
    current_num_points = 0;
    setScore();
    win_text.gameObject.SetActive(false);
  }

  public void AddNewItem(Transform _toAdd, int points)
  {
    // Debug.Log("Adding New Item...");
    _toAdd.DOJump(item_holder.position + new Vector3(0, y_position * current_num_points, 0), 1.5f, 1, 0.35f).OnComplete(() =>
    {
      current_num_points += points;
      setScore();
      _toAdd.SetParent(item_holder, true);
      //_toAdd.localPosition = new Vector3(0, Ypos * NumOfItemsHoldind, 0);
      //Temp fix for duel replace below with above later
      _toAdd.localPosition = new Vector3(0, -3, 0);
      _toAdd.localRotation = Quaternion.identity;

    });

  }

  public void AddItemToHotbar(UsableItem uItem)
  {
    hotbar.GetComponent<ItemHotbar>().AddItem(uItem);
  }

  public void setScore()
  {
    // Debug.Log("Setting score: " + num_items_held + " / " + num_items_to_win);
    score_text.text = "Food Collected: " + current_num_points.ToString() + " / " + num_points_to_win.ToString();
  }

  public bool CheckWin()
  {
    if (current_num_points >= num_points_to_win)
    {
      win_lose_controller.GetComponent<WinLoseControl>().WinGame();
      return true;
    }
    return false;
  }
}

