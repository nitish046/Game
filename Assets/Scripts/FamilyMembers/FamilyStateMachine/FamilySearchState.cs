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

    public FamilySearchState(FamilyMember family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        nav_mesh_member.destination = search_location;
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        nav_mesh_member.ResetPath();
    }
}
