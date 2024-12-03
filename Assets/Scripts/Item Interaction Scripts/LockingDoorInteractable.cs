using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LockingDoorInteractable : Interactable
{
  private Animator door_animator;
  private AudioSource[] door_noises;

  public bool isLocked;

  private bool messagePlaying = false;

  private void Awake()
  {
    door_animator = GetComponent<Animator>();
    door_noises = GetComponents<AudioSource>();
  }

  protected override void Interact()
  {
    if (isLocked)
    {
      Debug.Log("Door is locked.");
      if (!messagePlaying)
      {
        messagePlaying = true;
        StartCoroutine(doorIsLocked());
      }
      return;
    }
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

  private IEnumerator doorIsLocked()
  {
    float messageDuration = 3f;
    WaitForSeconds wait = new WaitForSeconds(messageDuration);
    string temp = prompt_message;
    prompt_message = "Oh dear, seems the door is locked. Maybe you should try some other time.";
    yield return wait;
    prompt_message = temp;
    messagePlaying = false;
  }
}
