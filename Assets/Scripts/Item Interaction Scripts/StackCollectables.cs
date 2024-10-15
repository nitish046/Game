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

  public GameObject winScreen;
  public GameObject mainScreen;
  public Button restart_button;
  public Button quit_button;

  public Transform item_holder;
  public int num_items_held;
  public float y_position;


  private void Start()
  {
    num_items_held = 0;
    setScore();
    win_text.gameObject.SetActive(false);
  }

  public void AddNewItem(Transform _toAdd)
  {
    _toAdd.DOJump(item_holder.position + new Vector3(0, y_position * num_items_held, 0), 1.5f, 1, 0.35f).OnComplete(() =>
    {
      num_items_held++;
      setScore();
      _toAdd.SetParent(item_holder, true);
      //_toAdd.localPosition = new Vector3(0, Ypos * NumOfItemsHoldind, 0);
      //Temp fix for duel replace below with above later
      _toAdd.localPosition = new Vector3(0, -3, 0);
      _toAdd.localRotation = Quaternion.identity;

    });

  }

  public void setScore()
  {
    score_text.text = "Food Collected: " + num_items_held.ToString() + " /2";
    if (num_items_held == 2)
    {
      // win_text.gameObject.SetActive(true);
      mainScreen.SetActive(false);
      winScreen.SetActive(true);
      restart_button.gameObject.SetActive(true);
      quit_button.gameObject.SetActive(true);
    }
  }
}

