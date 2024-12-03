using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmilyActivatedState : FamilyBaseState
{
    private GameObject player;

    private float charge_timer;
    private float charge_cooldown;

    private float distance_to_player;
    private float follow_distance;

    private EmilyStateMachine emily_state_machine;

    private readonly EmilyController member;
    private Animator member_animator;
    private NavMeshAgent nav_mesh_member;

    public EmilyActivatedState(EmilyController family_member, Animator animator, NavMeshAgent nav_mesh_agent)
    {
        member = family_member;
        member_animator = animator;
        nav_mesh_member = nav_mesh_agent;
    }

    public override void EnterState()
    {
        emily_state_machine = (EmilyStateMachine)member.stateMachine;
        charge_cooldown = member.charge_cooldown;
        follow_distance = member.follow_distance;
        player = member.player;

        // Notify HouseMusic that Emily is activated
        member.OnActivated();
    }

    public override void UpdateState()
    {
        charge_timer += Time.deltaTime;
        member.transform.LookAt(player.transform);
        KeepInRange();
        if (charge_timer > charge_cooldown)
        {
            emily_state_machine.ChangeState(emily_state_machine.charge_state);
        }
    }

    public override void ExitState()
    {
        nav_mesh_member.ResetPath();
        charge_timer = 0;
        member_animator.ResetTrigger("isWalking");
        member_animator.SetTrigger("isIdle");

        // Notify HouseMusic that Emily is deactivated
        member.OnDeactivated();
    }

    private void KeepInRange()
    {
        distance_to_player = Vector3.Distance(member.transform.position, player.transform.position);

        if (distance_to_player > follow_distance)
        {
            member_animator.ResetTrigger("isIdle");
            member_animator.SetTrigger("isWalking");
            nav_mesh_member.SetDestination(player.transform.position);
        }
        else
        {
            member_animator.ResetTrigger("isWalking");
            member_animator.SetTrigger("isIdle");
            nav_mesh_member.ResetPath();
        }
    }
}
