using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FieldOfView : MonoBehaviour  // The copy of this script included in the FamilyMember class is what does the work.
                                          // The copy of this attached to Henry in the scene is just for debugging purposes, 
{                                         // to allow the Editor script to show the visible Field of View you can see in the scene view
  public float radius;
  public float viewAngle = 120;
  public float periferalAngle = 190;

  public GameObject playerRef;
  public GameObject familyMember;

  public LayerMask targetMask;
  public LayerMask obstructionMask;

  public bool canSeePlayer;

  public FieldOfView(GameObject player, GameObject member, float viewRadius, float vAngle, float pAngle, LayerMask tMask, LayerMask oMask)
  {
    playerRef = player;
    familyMember = member;
    radius = viewRadius;
    viewAngle = vAngle;
    periferalAngle = pAngle;
    targetMask = tMask;
    obstructionMask = oMask;
  }

  public IEnumerator FOVRoutine()
  {
    float delay = 0.2f;
    WaitForSeconds wait = new WaitForSeconds(delay);

    while (true)
    {
      //Debug.Log("0.2 second loop");
      yield return wait;
      FieldOfViewCheck();
    }
  }

  private void FieldOfViewCheck()
  {
    Transform fTransform = familyMember.transform;
    //Debug.Log(fTransform.ToString());
    Collider[] rangeChecks = Physics.OverlapSphere(fTransform.position, radius, targetMask);
    //Debug.Log(".\n\n\n.");
    //Debug.Log("fTransform.position: " + fTransform.position + "\nradius: " + radius + "\ntargetMask: " + targetMask);
    //Debug.Log(Physics.OverlapSphere(fTransform.position, radius, targetMask).Length);

    if (rangeChecks.Length > 0)
    {
      //Debug.Log("1 - rangeChecks.Length > 0");
      Transform target = rangeChecks[0].transform;
      Vector3 directionToTarget = (target.position - fTransform.position).normalized;

      if (Vector3.Angle(fTransform.forward, directionToTarget) < viewAngle / 2)
      {
        //Debug.Log("2 - Vector3.Angle(fTransform.forward, directionToTarget) < viewAngle / 2");
        float distanceToTarget = Vector3.Distance(fTransform.position, target.position);

        if (!Physics.Raycast(fTransform.position, directionToTarget, distanceToTarget, obstructionMask))
        {
          //Debug.Log("!Physics.Raycast(fTransform.position, directionToTarget, distanceToTarget, obstructionMask)");
          canSeePlayer = true;
        }
        else { canSeePlayer = false; }
      }
      else { canSeePlayer = false; }
    }
    else if (canSeePlayer) { canSeePlayer = false; }
  }
}
