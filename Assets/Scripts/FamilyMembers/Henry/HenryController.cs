using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HenryController : FamilyMember
{

  [SerializeField] Transform path;
  [SerializeField] private float movement_speed = 5f;
  [SerializeField] private float rotation_speed = 90f;
  protected override float MovementSpeed => movement_speed;
  protected override float RotationSpeed => rotation_speed;


  // [SerializeField] float waypoint_size = .4f;
  // [SerializeField] float waypoint_wait_time = 2f;
  [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;



  // public GameObject loseScreen;
  // public GameObject mainScreen;
  // public Button restart_button;
  // public Button quit_button;
  public TMP_Text lose_text;
  public Material MainColor, FreezeColor;
  public float duration = 5f;
  // public bool allow;
  public AudioSource splash;
  [SerializeField] private HideOnCollide collision_occur;


  // private Animator animator;

  protected override void Start()
  {
    base.Start();
    collision_occur.onRaccoonFirstTimeOnTrash += collisionOccur_onRaccoonFirstTimeOnTrash;
  }


  private void collisionOccur_onRaccoonFirstTimeOnTrash(object sender, System.EventArgs e)
  {
    UnityEngine.Debug.Log("collisionOccur_onRaccoonFirstTimeOnTrash");
    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    familyMemberState = FamilyMemberState.PATROL;
    // StartCoroutine(Patrol(getWaypointArray()));
    patrolCoroutine = StartCoroutine(Patrol(getWaypointArray("Patrol")));
    walkingTransition(true);
  }

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

  // IEnumerator Patrol(Vector3[] waypoints)
  // {
  //   int waypoint_index = 0;
  //   Vector3 waypoint_target = waypoints[waypoint_index];

  //   while (true)
  //   {
  //     if (allow)
  //     {
  //       transform.position = Vector3.MoveTowards(transform.position, waypoint_target, movement_speed * Time.deltaTime);
  //       transform.LookAt(waypoint_target);

  //       if (transform.position == waypoint_target)
  //       {
  //         walkingTransition(false);
  //         waypoint_index = (waypoint_index + 1) % waypoints.Length;
  //         waypoint_target = waypoints[waypoint_index];
  //         yield return new WaitForSeconds(waypoint_wait_time);
  //         yield return StartCoroutine(turnTowardsPosition(waypoint_target));
  //         walkingTransition(true);
  //       }
  //     }
  //     yield return null;
  //   }
  // }




  IEnumerator turnTowardsPosition(Vector3 rotation_target)
  {
    Vector3 direction = (rotation_target - transform.position).normalized;
    float target_angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

    while (Mathf.DeltaAngle(transform.eulerAngles.y, target_angle) > Mathf.Abs(0.05f))
    {
      float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, target_angle, rotation_speed * Time.deltaTime);
      transform.eulerAngles = Vector3.up * angle;
      yield return null;
    }
  }

  private void walkingTransition(bool walking)
  {
    if (walking)
    {
      animator.SetBool("isWalking", true);
    }
    else
    {
      animator.SetBool("isWalking", false);
    }
  }

  protected Vector3[] getWaypointArray()
  {
    return base.getWaypointArray(path);
  }
  protected Vector3[] getWaypointArray(string type)
  {
    UnityEngine.Debug.Log("Henry getWaypointArray type");
    return base.getWaypointArray("henry", type);
  }

  private void OnDrawGizmos()
  {
    base.OnDrawGizmos(path);
  }
}

