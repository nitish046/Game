using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetInteractable : Interactable
{
    private Animator cabinet_animator;

    private void Awake()
    {
        cabinet_animator = GetComponentInChildren<Animator>();
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with");
        bool isOpen = cabinet_animator.GetBool("IsOpen");
        if (isOpen)
        {
            cabinet_animator.SetBool("IsOpen", false);
        }
        else
        {
            cabinet_animator.SetBool("IsOpen", true);
        }
    }
}
