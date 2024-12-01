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


  [SerializeField] private float radius;
  [SerializeField] private float viewAngle;

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

  // private void detectInteraction()
  // {
  //   Collider[] rangeChecks = Physics.OverlapSphere(base.transform.position, radius, mask);
  //   Debug.Log(rangeChecks.Length);
  //   if (rangeChecks.Length > 0)
  //   {
  //     for (int i = 0; i < rangeChecks.Length; i++)
  //     {
  //       Debug.Log("i = " + i);
  //       Transform target = rangeChecks[i].transform;
  //       Vector3 directionToTarget = (target.position - base.transform.position).normalized;

  //       if (Vector3.Angle(base.transform.forward, directionToTarget) < viewAngle / 2)
  //       {
  //         Debug.Log("in angle");
  //         float distanceToTarget = Vector3.Distance(base.transform.position, target.position);

  //         RaycastHit hitInfo;

  //         if (Physics.Raycast(base.transform.position, directionToTarget, out hitInfo, distance, mask))
  //         {
  //           Debug.Log("raycast hit");
  //           promptInteraction(hitInfo);
  //           i = rangeChecks.Length;
  //         }
  //       }
  //     }
  //   }
  // }


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


