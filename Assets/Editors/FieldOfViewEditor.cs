#if (UNITY_EDITOR)

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
  private void OnSceneGUI()
  {
    FieldOfView fov = (FieldOfView)target;

    Handles.color = Color.white;
    Handles.DrawWireArc(fov.familyMember.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

    Vector3 viewAngle1 = getDirectionFromAngle(fov.familyMember.transform.eulerAngles.y, -fov.viewAngle / 2);
    Vector3 viewAngle2 = getDirectionFromAngle(fov.familyMember.transform.eulerAngles.y, fov.viewAngle / 2);
    Vector3 viewAngle3 = getDirectionFromAngle(fov.familyMember.transform.eulerAngles.y, -fov.periferalAngle / 2);
    Vector3 viewAngle4 = getDirectionFromAngle(fov.familyMember.transform.eulerAngles.y, fov.periferalAngle / 2);

    Handles.color = Color.red;
    Handles.DrawLine(fov.familyMember.transform.position, fov.familyMember.transform.position + viewAngle1 * fov.radius);
    Handles.DrawLine(fov.familyMember.transform.position, fov.familyMember.transform.position + viewAngle2 * fov.radius);

    Handles.color = Color.yellow;
    Handles.DrawLine(fov.familyMember.transform.position, fov.familyMember.transform.position + viewAngle3 * fov.radius);
    Handles.DrawLine(fov.familyMember.transform.position, fov.familyMember.transform.position + viewAngle4 * fov.radius);

    if (fov.canSeePlayer)
    {
      Handles.DrawLine(fov.familyMember.transform.position, fov.playerRef.transform.position);
    }
  }

  private Vector3 getDirectionFromAngle(float EulerY, float angleInDegrees)
  {
    angleInDegrees += EulerY;

    return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
  }
}

#endif