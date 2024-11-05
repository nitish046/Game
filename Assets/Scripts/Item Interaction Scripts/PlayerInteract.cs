using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float distance = 1f;
    [SerializeField] LayerMask mask;
    [SerializeField] private GameInput game_input;

    private InteractionUI interaction_UI;
    private bool interact = false;

    private void Awake()
    {
        interaction_UI = GetComponent<InteractionUI>();
        game_input.on_interact_action += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        interact = true;
    }

    private void Update()
    {
        detectInteraction();
    }

    private void detectInteraction()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position + transform.up, transform.forward);

        interaction_UI.updatePromptText(string.Empty);

        Debug.DrawRay(ray.origin, ray.direction * distance);
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            promptInteraction(hitInfo);
        }
    }

    
    private void promptInteraction(RaycastHit hitInfo)
    {
        if (hitInfo.collider.GetComponent<Interactable>() != null)
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            interaction_UI.updatePromptText(interactable.prompt_message);
            if (interact)
            {
                //Debug.Log("Interacted with");
                interactable.BaseInteract();
                interact = false;
            }
        }
    }
}

    
