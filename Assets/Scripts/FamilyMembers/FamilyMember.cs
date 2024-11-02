using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public abstract class FamilyMember : MonoBehaviour
{
  public enum FamilyMemberState
  {
    NORMAL,
    PATROL,
    ACTIVATED
  }

  protected abstract float MovementSpeed { get; }
  protected abstract float RotationSpeed { get; }

  [SerializeField] float distance;

  [SerializeField] protected float waypoint_size = .4f;
  [SerializeField] protected float waypoint_wait_time = 2f;
  public bool allow;

  [SerializeField] GameObject player;

  protected Animator animator;
  [SerializeField] protected GameObject win_lose_controller;

  [SerializeField] protected FamilyMemberState familyMemberState = FamilyMemberState.NORMAL;

  protected FieldOfView fieldOfView;
  [SerializeField] protected float viewRadius;
  [Range(0, 360)][SerializeField] protected float viewAngle = 120;
  [Range(0, 360)][SerializeField] protected float periferalAngle = 190;
  [SerializeField] protected LayerMask targetMask;
  [SerializeField] protected LayerMask obstructionMask;

  public bool hasSeenPlayer = false;
  public float patrolDuration = 10;
  public float secondsSinceSeenPlayer = 0;
  protected Coroutine timerCoroutine;
  bool isTimerCoroutineRunning = false;

  public GameObject patrolPointOperator;
  protected Coroutine patrolCoroutine;
  bool isPatrolCoroutineRunning = false;


  protected virtual void Start()
  {
    // collision_occur.onRaccoonFirstTimeOnTrash += collisionOccur_onRaccoonFirstTimeOnTrash;
    allow = true;
    animator = transform.GetChild(0).GetComponent<Animator>();
    fieldOfView = new FieldOfView(player, this.gameObject, viewRadius, viewAngle, periferalAngle, targetMask, obstructionMask);
    StartCoroutine(fieldOfView.FOVRoutine());
  }

  protected void Update()
  {
    distance = Vector3.Distance(transform.position, player.transform.position);
    CheckForRaccoon();
  }

  protected void CheckForRaccoon()
  {
    if (fieldOfView.canSeePlayer)
    {
      hasSeenPlayer = true;
      if (isTimerCoroutineRunning)
      {
        isTimerCoroutineRunning = false;
        StopCoroutine(timerCoroutine);
      }
      SeesRaccoon();
    }
    else if (hasSeenPlayer)
    {
      familyMemberState = FamilyMemberState.PATROL;
      hasSeenPlayer = false;
      // Go on Patrol, start timer with secondsSinceSeenPlayer
      timerCoroutine = StartCoroutine(seePlayerTimer());
    }
  }

  protected IEnumerator seePlayerTimer()
  {
    isTimerCoroutineRunning = true;
    WaitForSeconds wait = new WaitForSeconds(1.0f);
    while (!fieldOfView.canSeePlayer)
    {
      yield return wait;
      secondsSinceSeenPlayer++;
      if (secondsSinceSeenPlayer >= 10)
      {
        familyMemberState = FamilyMemberState.NORMAL;
        isPatrolCoroutineRunning = false;
        StopCoroutine(patrolCoroutine);
        //call to implement normal behavior
        isTimerCoroutineRunning = false;
        yield break;
      }
    }
    familyMemberState = FamilyMemberState.ACTIVATED;
    isTimerCoroutineRunning = false;
    secondsSinceSeenPlayer = 0;
  }

  protected void SeesRaccoon()
  {
    // restart_button.gameObject.SetActive(true);
    // quit_button.gameObject.SetActive(true);
    // mainScreen.SetActive(false);
    // loseScreen.SetActive(true);
    win_lose_controller.GetComponent<WinLoseControl>().LoseGame();
    if (familyMemberState == FamilyMemberState.PATROL && isPatrolCoroutineRunning)
    {
      StopCoroutine(patrolCoroutine);
      isPatrolCoroutineRunning = false;
    }
    familyMemberState = FamilyMemberState.ACTIVATED;
  }

  public abstract void Freeze(float freezeDuration, bool isTrapFreeze);

  protected IEnumerator Patrol(Vector3[] waypoints)
  {
    isPatrolCoroutineRunning = true;
    familyMemberState = FamilyMemberState.PATROL;
    int waypoint_index = 0;
    Vector3 waypoint_target = waypoints[waypoint_index];

    while (true)
    {
      if (allow)
      {
        transform.position = Vector3.MoveTowards(transform.position, waypoint_target, MovementSpeed * Time.deltaTime);
        transform.LookAt(waypoint_target);

        if (transform.position == waypoint_target)
        {
          walkingTransition(false);
          waypoint_index = (waypoint_index + 1) % waypoints.Length;
          waypoint_target = waypoints[waypoint_index];
          yield return new WaitForSeconds(waypoint_wait_time);
          yield return StartCoroutine(turnTowardsPosition(waypoint_target));
          walkingTransition(true);
        }
      }
      yield return null;
    }
  }




  IEnumerator turnTowardsPosition(Vector3 rotation_target)
  {
    Vector3 direction = (rotation_target - transform.position).normalized;
    float target_angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

    while (Mathf.DeltaAngle(transform.eulerAngles.y, target_angle) > Mathf.Abs(0.05f))
    {
      float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, target_angle, RotationSpeed * Time.deltaTime);
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

  protected virtual Vector3[] getWaypointArray(Transform path)
  {
    Vector3[] waypoint_array = new Vector3[path.childCount];
    for (int i = 0; i < waypoint_array.Length; i++)
    {
      waypoint_array[i] = path.GetChild(i).position;
    }

    return waypoint_array;
  }
  protected virtual Vector3[] getWaypointArray(string name, string type)
  {
    Vector3[] waypoint_array = patrolPointOperator.GetComponent<FamilyPatrolPoints>().findPatrolPoints(name, type);

    return waypoint_array;
  }

  protected void OnDrawGizmos(Transform path)
  {
    Vector3 start_waypoint_position = path.GetChild(0).position;
    Vector3 previous_waypoint_position = start_waypoint_position;

    foreach (Transform waypoint in path)
    {
      Gizmos.color = new Color(238f / 255, 130f / 255, 238f / 255, 255f / 255);
      Gizmos.DrawSphere(waypoint.position, waypoint_size);

      Gizmos.color = Color.white;
      Gizmos.DrawLine(previous_waypoint_position, waypoint.position);

      previous_waypoint_position = waypoint.position;
    }
    Gizmos.DrawLine(previous_waypoint_position, start_waypoint_position);
  }
}
