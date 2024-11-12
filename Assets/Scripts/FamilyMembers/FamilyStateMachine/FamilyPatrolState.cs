using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class FamilyPatrolState : FamilyBaseState
{
    private readonly FamilyMember member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    private int waypoint_index = 0;
    //Vector3 waypoint_target;

    private Coroutine patrolCoroutine;


    public FamilyPatrolState(FamilyMember family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }
    public override void EnterState()
    {
        member_animator.enabled = true;
        //waypoint_target = member.waypoint_array[waypoint_index];
        patrolCoroutine = member.StartCoroutine(Patrol(member.waypoint_array));
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        if (patrolCoroutine != null)
        {
            member.StopCoroutine(patrolCoroutine);
        }
        member_animator.enabled = false;
        if (nav_mesh_member != null) nav_mesh_member.ResetPath();
    }

    protected IEnumerator Patrol(Vector3[] waypoints)
    {
        Vector3 waypoint_target = waypoints[waypoint_index];
        while (true)
        {
            //member.transform.position = Vector3.MoveTowards(member.transform.position, waypoint_target, member.MovementSpeed * Time.deltaTime);
            //member.transform.LookAt(waypoint_target);
            nav_mesh_member.destination = waypoint_target;

            if (nav_mesh_member.remainingDistance < 0.2f)
            {
                walkingTransition(false);

                waypoint_index = (waypoint_index + 1) % waypoints.Length;
                waypoint_target = waypoints[waypoint_index];

                yield return new WaitForSeconds(member.waypoint_wait_time);
                //turnTowardsCoroutine = member.StartCoroutine(turnTowardsPosition(waypoint_target));

                walkingTransition(true);
            }
            yield return null;
        }
    }

    private void walkingTransition(bool walking)
    {
        if (walking)
        {
            member_animator.SetBool("isWalking", true);
        }
        else
        {
            member_animator.SetBool("isWalking", false);
        }
    }
}
