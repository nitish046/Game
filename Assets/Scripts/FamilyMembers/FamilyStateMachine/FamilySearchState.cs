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
    private float seconds_since_rotated;

    private bool is_rotating;
    private Quaternion target_rotation;

    private float search_time = 10f;
    private float rotation_time;
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
        set_rotation_time();
        is_rotating = false;
        nav_mesh_member.destination = search_location;
        member_animator.ResetTrigger("isIdle");
        member_animator.SetTrigger("isWalking");
    }

    public override void UpdateState()
    {
        if(reached_search_location)
        {
            SeePlayerTimer();
            RotateRandomly();
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
        seconds_since_rotated = 0;
        reached_search_location = false;

        // Add this to reset animations
        member_animator.ResetTrigger("isIdle");
        member_animator.ResetTrigger("isWalking");
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
            member_animator.ResetTrigger("isWalking");
            member_animator.SetTrigger("isIdle");
        }
    }

    private void RotateRandomly()
    {
        if(is_rotating)
        {
            member.transform.rotation = Quaternion.Lerp(member.transform.rotation,target_rotation,Time.deltaTime * 2f);
            if (Quaternion.Angle(member.transform.rotation, target_rotation) < 1f)
            {
                is_rotating = false;
                set_rotation_time();
            }
        }
        else
        {
            seconds_since_rotated += Time.deltaTime;
            if (seconds_since_rotated >= rotation_time)
            {
                float random_angle = UnityEngine.Random.Range(0f, 360f);
                target_rotation = Quaternion.Euler(0, random_angle, 0);

                is_rotating = true;
                seconds_since_rotated = 0;
            }
        }
    }

    private void set_rotation_time()
    {
        rotation_time = Random.Range(.2f, .5f);
    }
}
