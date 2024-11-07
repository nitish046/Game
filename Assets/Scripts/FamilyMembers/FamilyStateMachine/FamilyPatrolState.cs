using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static FamilyMember;
using UnityEngine.AI;

public class FamilyPatrolState : FamilyBaseState
{
    private Animator animator;
    private Vector3[] waypoint_array;
    private int waypoint_index = 0;
    public override void EnterState(HenryStateMachine henry)
    {
        animator = henry.transform.GetChild(0).GetComponent<Animator>();
        waypoint_array = henry.GetComponent<FamilyMember>().waypoint_array;
        animator.enabled = true;
        henry.StartCoroutine(Patrol(henry,waypoint_array));
    }

    public override void UpdateState(HenryStateMachine henry)
    {

    }

    public override void ExitState(HenryStateMachine henry)
    {
        henry.StopAllCoroutines();
        animator.enabled = false;
    }

    protected IEnumerator Patrol(HenryStateMachine henry, Vector3[] waypoints)
    {
        UnityEngine.Debug.Log("Entering Henry Patrol");
        //int waypoint_index = 0;
        Vector3 waypoint_target = waypoints[waypoint_index];
        while (true)
        {
                henry.transform.position = Vector3.MoveTowards(henry.transform.position, waypoint_target, henry.movement_speed * Time.deltaTime);
                henry.transform.LookAt(waypoint_target);

                if (henry.transform.position == waypoint_target)
                {
                    walkingTransition(false);

                    waypoint_index = (waypoint_index + 1) % waypoints.Length;
                    waypoint_target = waypoints[waypoint_index];

                    yield return new WaitForSeconds(henry.waypoint_wait_time);
                    yield return henry.StartCoroutine(turnTowardsPosition(henry, waypoint_target));

                    walkingTransition(true);
                }
            yield return null;
        }
    }




    IEnumerator turnTowardsPosition(HenryStateMachine henry, Vector3 rotation_target)
    {
        Vector3 direction = (rotation_target - henry.transform.position).normalized;
        float target_angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        while (Mathf.DeltaAngle(henry.transform.eulerAngles.y, target_angle) > Mathf.Abs(0.05f))
        {
            float angle = Mathf.MoveTowardsAngle(henry.transform.eulerAngles.y, target_angle, henry.rotation_speed * Time.deltaTime);
            henry.transform.eulerAngles = Vector3.up * angle;
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
}
