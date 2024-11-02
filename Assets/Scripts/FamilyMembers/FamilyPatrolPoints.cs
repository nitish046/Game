using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyPatrolPoints : MonoBehaviour
{
  [SerializeField] private Vector3[] henryPatrolPoints;
  [SerializeField] private Vector3[] emilyPatrolPoints;
  [SerializeField] private Vector3[] motherDearPatrolPoints;
  [SerializeField] private Vector3[] emilyNormalPoints;
  [SerializeField] private Vector3[] motherDearNormalPoints;

  public Vector3[] findPatrolPoints(string name, string type)
  {
    return this.GetType().GetField(name + type + "Points").GetValue(this) as Vector3[];
  }
}
