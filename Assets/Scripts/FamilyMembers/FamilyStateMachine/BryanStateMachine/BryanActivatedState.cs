using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BryanActivatedState : FamilyBaseState
{
    private readonly BryanController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    private Vector3 guard_position;
    private float distance_to_player;
    private float attack_range;

    public BryanActivatedState(BryanController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        guard_position = member.guardPosition.transform.position;
        attack_range = member.attack_range;
        nav_mesh_member.SetDestination(guard_position);
    }

    public override void UpdateState()
    {
        distance_to_player = Vector3.Distance(member.transform.position, member.player.transform.position);
        if(nav_mesh_member.remainingDistance <= 0.2f)
        {
            InAttackRange();
        }
    }

    public override void ExitState()
    {
        nav_mesh_member.ResetPath();
    }

    private void InAttackRange()
    {
        if (distance_to_player <= attack_range)
        {
            member.stateMachine.ChangeState(member.stateMachine.attack_state);
        }
    }
}
