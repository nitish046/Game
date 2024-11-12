using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyPatrolPoints : MonoBehaviour
{
  public Vector3[] henryPatrolPoints;
  public Vector3[] emilyPatrolPoints;
  [SerializeField] private Vector3[] motherDearPatrolPoints;
  [SerializeField] private Vector3[] emilyNormalPoints;
  [SerializeField] private Vector3[] motherDearNormalPoints;

  public Vector3[] findPatrolPoints(string name, string type)
  {
    // UnityEngine.Debug.Log("findPatrolPoints");
    // UnityEngine.Debug.Log(this.GetType());
    // UnityEngine.Debug.Log(this.GetType().GetField("henryPatrolPoints"));
    // UnityEngine.Debug.Log(this.GetType().GetField(name + type + "Points"));
    // UnityEngine.Debug.Log(this.GetType().GetField(name + type + "Points").GetValue(this));
    // UnityEngine.Debug.Log(this.GetType().GetField(name + type + "Points").GetValue(this) as Vector3[]);
    return this.GetType().GetField(name + type + "Points").GetValue(this) as Vector3[];
  }

  // public Vector3 findStartingPosition(string name, string type)
  // {
  //   return (this.GetType().GetField(name + type + "Points").GetValue(this) as Vector3[])[0];
  // }
}
