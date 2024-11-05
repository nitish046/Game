using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool use_events;
    public string prompt_message;

    public void BaseInteract()
    {
        if (use_events)
        {
            GetComponent<InteractionEvent>().on_interact.Invoke();
        }
        Interact();
    }
    protected virtual void Interact()
    {

    }
}
