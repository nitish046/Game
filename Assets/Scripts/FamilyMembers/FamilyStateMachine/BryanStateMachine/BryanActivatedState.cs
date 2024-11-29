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

    private bool done_walking = false;

    private GameObject player;

    public BryanActivatedState(BryanController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        member.katana.GetComponent<Renderer>().enabled = true;
        guard_position = member.guardPosition.transform.position;
        attack_range = member.attack_range;
        player = member.player;
        nav_mesh_member.SetDestination(guard_position);
        member_animator.SetTrigger("isWalking");
    }

    public override void UpdateState()
    {
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);
        if(nav_mesh_member.remainingDistance <= 0.2f)
        {
            if(!done_walking)
            {
                member_animator.ResetTrigger("isWalking");
                member_animator.SetTrigger("isIdle");
                done_walking = true;
            }
            InAttackRange();
            member.transform.LookAt(player.transform);
        }
    }

    public override void ExitState()
    {
        member_animator.ResetTrigger("isIdle");
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
