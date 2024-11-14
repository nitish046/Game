using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManagerSignalerWinScript : UIManagerSignalerScript
{
  protected StackCollectables stackCollectables;
  [SerializeField] protected GameObject hasStackCollectibles;

  private void Start()
  {
    stackCollectables = hasStackCollectibles.GetComponent<StackCollectables>();
  }

  public override void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (!stackCollectables.CheckWin())
      {
        UI_manager.setTutorialText(text);
      }
    }
  }
}
