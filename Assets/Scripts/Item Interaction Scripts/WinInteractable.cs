using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WinInteractable : Interactable
{
  [SerializeField] protected GameObject winCheck;
  private int patience = 3;
  private bool messagePlaying = false;

  private void Awake()
  {

  }

  protected override void Interact()
  {
    Debug.Log("Interacted with");
    if (!winCheck.GetComponent<StackCollectables>().CheckWin() && !messagePlaying)
    {
      messagePlaying = true;
      StartCoroutine(haveNotWonYet());
    }
  }

  private IEnumerator haveNotWonYet()
  {
    float messageDuration = 3f;
    WaitForSeconds wait = new WaitForSeconds(messageDuration);
    string temp = prompt_message;
    patience--;
    if (patience <= 0)
    {
      prompt_message = "I have already said that you do not have enough food to leave yet.<br>Look, see that little number up there in the corner? No, the other corner. Yeah, that one. That\'s your total food. Come back when it's full.";
      patience = 3;
      yield return wait;
      yield return wait;
      yield return wait;
    }
    else
    {
      prompt_message = "You do not have enough food to leave yet.<br>Go find more, then come back.";
    }
    yield return wait;
    prompt_message = temp;
    messagePlaying = false;
  }
}
