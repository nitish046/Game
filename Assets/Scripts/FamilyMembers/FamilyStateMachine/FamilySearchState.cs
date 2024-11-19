using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FamilySearchState : FamilyBaseState
{
    private readonly FamilyMember member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;
    
    public Vector3 search_location;
    private float seconds_since_seen_player;
    private float search_time = 5f;
    private bool reached_search_location;

    public FamilySearchState(FamilyMember family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        reached_search_location = false;
        nav_mesh_member.destination = search_location;
    }

    public override void UpdateState()
    {
        if(reached_search_location)
        {
            SeePlayerTimer();
        }
        else
        {
            IsAtDestination();
        }
    }

    public override void ExitState()
    {
        nav_mesh_member.ResetPath();
        seconds_since_seen_player = 0;
        reached_search_location = false;
    }

    protected void SeePlayerTimer()
    {
            seconds_since_seen_player += Time.deltaTime;
            if (seconds_since_seen_player >= search_time)
            {
                member.stateMachine.ChangeState(member.stateMachine.patrol_state);
            }
    }

    protected void IsAtDestination()
    {
        if (!nav_mesh_member.pathPending && nav_mesh_member.remainingDistance <= nav_mesh_member.stoppingDistance)
        {
            reached_search_location = true;
        }
    }
}
