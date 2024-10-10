using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManagerSignalerScript : MonoBehaviour
{
  private bool hasOccured = false;
  [SerializeField] string text = " ";
  [SerializeField] TutorialUIManager UIManager;

  void OnTriggerEnter(Collider other)
  {
    if (!hasOccured && other.CompareTag("Player"))
    {
      UIManager.setTutorialText(text);
    }
  }
}
