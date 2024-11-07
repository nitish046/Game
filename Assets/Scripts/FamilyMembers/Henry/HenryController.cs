using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HenryController : FamilyMember
{

  [SerializeField] private float movement_speed = 5f;
  [SerializeField] private float rotation_speed = 90f;
  protected override float MovementSpeed => movement_speed;
  protected override float RotationSpeed => rotation_speed;

  public GameObject hammer;

  public TMP_Text lose_text;
  public Material MainColor, FreezeColor;
  public float duration = 5f;
  public AudioSource splash;
  [SerializeField] private HideOnCollide collision_occur;
  private HenryStateMachine stateMachine;
  protected override void Start()
  {
    base.Start();
    stateMachine = GetComponent<HenryStateMachine>();
    HenryStartInHouse();
  }


  private void collisionOccur_onRaccoonFirstTimeOnTrash(object sender, System.EventArgs e)
  {
    // UnityEngine.Debug.Log("collisionOccur_onRaccoonFirstTimeOnTrash");
    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    stateMachine.enabled = true;
    //stateMachine.ChangeState(stateMachine.patrol_state);
    //patrolCoroutine = StartCoroutine(Patrol(getWaypointArray("Patrol")));
  }
  private void HenryStartInHouse()
  {
    waypoint_array = getWaypointArray("Patrol");
    transform.position = waypoint_array[0];
    stateMachine.enabled = true;
  }

  protected override void SeesRaccoon()
  {
    base.SeesRaccoon();
    stateMachine.ChangeState(stateMachine.activated_state);
  }
  /*
public override void Freeze(float freezeDuration, bool isTrapFreeze)
{
  duration = freezeDuration; // Set the freeze duration based on the trap
  allow = false; // Stop movement

  // Only change color if it's not a trap freeze
  if (!isTrapFreeze)
  {
    skinnedMeshRenderer.material = FreezeColor; // Change to FreezeColor
  }

  if (splash != null && splash.clip != null)
  {
    splash.Play();
  }
  else
  {
    UnityEngine.Debug.LogWarning("Splash AudioSource or AudioClip is not assigned.");
  }

  // If it's a trap freeze, make Henry fall and pause animation
  if (isTrapFreeze)
  {
    animator.enabled = false; // Pause all animations
    transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Rotate Henry to appear as if he has fallen down
    UnityEngine.Debug.Log("Henry has been frozen and fallen to the ground.");
  }
  else
  {
    UnityEngine.Debug.Log("Henry has been frozen by another method.");
  }

  StartCoroutine(delay(isTrapFreeze)); // Pass the freeze type to the delay
}


IEnumerator delay(bool isTrapFreeze)
{
  yield return new WaitForSeconds(duration);

  skinnedMeshRenderer.material = MainColor; // Reset color
  allow = true;

  if (isTrapFreeze)
  {
    animator.enabled = true; // Resume animations
    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Reset rotation to stand Henry back up
                                                                                                                   //Debug.Log("Henry has unfrozen and is standing up.");
  }

  //Debug.Log("Henry has unfrozen.");
}

  */



  protected Vector3[] getWaypointArray(string type)
  {
    // UnityEngine.Debug.Log("Henry getWaypointArray type");
    return base.getWaypointArray("henry", type);
  }

}

