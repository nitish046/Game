using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ChairInteractable : Interactable
{
  private Transform chair_transform;
  [SerializeField] private bool is_pulled_out = false;
  //private Coroutine pull_chair;
  private bool pulling_chair;

  private void Awake()
  {
    chair_transform = GetComponent<Transform>();
    pulling_chair = false;
  }

  protected override void Interact()
  {
    Debug.Log("Interacted with Chair");

    if (!pulling_chair)
    {
      if (is_pulled_out)
      {
        StartCoroutine(pullingChair(1.0f));
      }
      else
      {
        StartCoroutine(pullingChair(-1.0f));
      }
    }

  }

  private IEnumerator pullingChair(float out_or_in)
  {
    pulling_chair = true;
    Debug.Log("Pulling chair " + (out_or_in < 0 ? "out" : "in"));
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    float rate = 0.01f * out_or_in;

    Debug.Log(rate);
    Debug.Log(chair_transform.forward);
    Debug.Log(chair_transform.forward * rate);
    for (int i = 0; i < 100; i++)
    {
      chair_transform.position = chair_transform.position + chair_transform.forward * rate;
      //chair_transform.Translate(chair_transform.forward * rate);
      Debug.Log("chair position: " + chair_transform.position);
      yield return wait;
    }
    is_pulled_out = (out_or_in < 0);
    Debug.Log("Done pulling chair " + (out_or_in < 0 ? "out" : "in"));
    pulling_chair = false;
  }
}
