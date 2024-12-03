using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DoorInteractable : Interactable
{
  private Animator door_animator;
  private AudioSource[] door_noises;

  private void Awake()
  {
    door_animator = GetComponent<Animator>();
    door_noises = GetComponents<AudioSource>();
  }

  protected override void Interact()
  {
    Debug.Log("Interacted with");
    bool isOpen = door_animator.GetBool("isOpen");
    if (isOpen)
    {
            door_animator.SetBool("isOpen", false);
            door_noises[0].Play();
    }
    else
    {
      door_animator.SetBool("isOpen", true);
      door_noises[1].Play();
    }
  }
}
