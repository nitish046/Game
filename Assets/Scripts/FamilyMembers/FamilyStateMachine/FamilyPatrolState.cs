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
        if (nav_mesh_member != null) nav_mesh_member.ResetPath();
    }

    protected IEnumerator Patrol(Vector3[] waypoints)
    {
        Vector3 waypoint_target = waypoints[waypoint_index];
        while (true)
        {
            nav_mesh_member.destination = waypoint_target;

            if (nav_mesh_member.remainingDistance < 0.2f)
            {
                member_animator.ResetTrigger("isWalking");
                member_animator.SetTrigger("isIdle");
                Debug.Log("idle");

                waypoint_index = (waypoint_index + 1) % waypoints.Length;
                waypoint_target = waypoints[waypoint_index];

                yield return new WaitForSeconds(member.waypoint_wait_time);

                member_animator.ResetTrigger("isIdle");
                member_animator.SetTrigger("isWalking");
                Debug.Log("walking");
            }
            yield return null;
        }
    }
}
