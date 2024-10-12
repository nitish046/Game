using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManagerSignalerScript : MonoBehaviour
{
  private bool has_occured = false;
  [SerializeField] string text = " ";
  [SerializeField] TutorialUIManager UI_manager;

  void OnTriggerEnter(Collider other)
  {
    if (!has_occured && other.CompareTag("Player"))
    {
        UI_manager.setTutorialText(text);
        has_occured = true;
    }
  }
}
