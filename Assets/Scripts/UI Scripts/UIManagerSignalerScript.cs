using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManagerSignalerScript : MonoBehaviour
{
  protected bool has_occured = false;
  [SerializeField] protected string text = " ";
  [SerializeField] protected TutorialUIManager UI_manager;

  public virtual void OnTriggerEnter(Collider other)
  {
    if (!has_occured && other.CompareTag("Player"))
    {
      UI_manager.setTutorialText(text);
      has_occured = true;
    }
  }
}
