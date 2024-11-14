using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DoorInteractable : Interactable
{
  private Animator door_animator;

  private void Awake()
  {
    door_animator = GetComponent<Animator>();
  }

  protected override void Interact()
  {
    Debug.Log("Interacted with");
    bool isOpen = door_animator.GetBool("isOpen");
    if (isOpen)
    {
      door_animator.SetBool("isOpen", false);
    }
    else
    {
      door_animator.SetBool("isOpen", true);
    }
  }
}
