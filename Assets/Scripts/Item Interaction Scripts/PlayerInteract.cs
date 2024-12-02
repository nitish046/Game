using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
using System;

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
    interaction_UI.updatePromptText("");
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

  // private void detectInteraction()
  // {
  //   RaycastHit hitInfo;
  //   Ray ray = new Ray(transform.position + transform.up, transform.forward);

  //   interaction_UI.updatePromptText(string.Empty);

  //   Debug.DrawRay(ray.origin, ray.direction * distance);
  //   if (Physics.Raycast(ray, out hitInfo, distance, mask))
  //   {
  //     promptInteraction(hitInfo);
  //   }
  // }

  private void detectInteraction()
  {
    Collider[] rangeChecks = Physics.OverlapSphere((base.transform.position + base.transform.up * 2f), radius, mask);
    if (rangeChecks.Length > 0)
    {
      Debug.Log(rangeChecks.Length);
      Array.Sort(rangeChecks, (colliderA, colliderB) =>
        {
          Transform targetA = colliderA.transform;
          Transform targetB = colliderB.transform;

          Vector3 directionToTargetA = (targetA.position - base.transform.position).normalized;
          Vector3 directionToTargetB = (targetB.position - base.transform.position).normalized;

          float angleA = XZPlaneAngle(base.transform.forward, directionToTargetA);
          float angleB = XZPlaneAngle(base.transform.forward, directionToTargetB);

          return angleA.CompareTo(angleB); // Sort by ascending angle
        });

      Transform target = rangeChecks[0].transform;
      Vector3 directionToTarget = (target.position - base.transform.position).normalized;

      if (XZPlaneAngle(base.transform.forward, directionToTarget) < viewAngle / 2)
      {
        Debug.Log("in angle");
        float distanceToTarget = Vector3.Distance(base.transform.position, target.position);

        if (Physics.Raycast(base.transform.position, directionToTarget, out RaycastHit hitInfo, distance, mask))
        {
          Debug.Log("raycast hit");
          promptInteraction(hitInfo); // Use PromptInteraction on the most aligned collider
        }
      }
      else
      {
        interaction_UI.updatePromptText("");
      }
    }
  }

  private float XZPlaneAngle(Vector3 vecOne, Vector3 vecTwo)
  {
    Vector3 vecone = new Vector3(vecOne.x, 0, vecOne.z);
    Vector3 vectwo = new Vector3(vecTwo.x, 0, vecTwo.z);
    return Vector3.Angle(vecone, vectwo);
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


