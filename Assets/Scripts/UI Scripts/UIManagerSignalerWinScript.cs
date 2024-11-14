using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManagerSignalerWinScript : UIManagerSignalerScript
{
  private bool hasEnoughFood;
  protected StackCollectables stackCollectables;
  [SerializeField] protected GameObject hasStackCollectibles;

  private void Start()
  {
    stackCollectables = hasStackCollectibles.GetComponent<StackCollectables>();
    StartCoroutine(checkDeclareEnoughFood());
  }

  private IEnumerator checkDeclareEnoughFood()
  {
    float timeBetweenChecks = 0.2f;
    WaitForSeconds wait = new WaitForSeconds(timeBetweenChecks);
    while (true)
    {
      if (hasEnoughFood)
      {
        break;
      }
      else if (stackCollectables.num_items_held >= stackCollectables.num_items_to_win)
      {
        hasEnoughFood = true;
        UI_manager.setTutorialText("You have enough food! Quick, run back to where you came from!");
      }
      yield return wait;
    }
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
